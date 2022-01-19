using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// TODO: Validation for the whole page.

namespace afterlife_caretakers.Pages.prefuneral
{
    public class resting_placeModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        private readonly Services.CasketService _csvc;
        public resting_placeModel(Services.FuneralService service, Services.CasketService cservice, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            _csvc = cservice;
            webHostEnvironment = hostEnvironment;
        }
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Funeral Funeral { get; set; }
        [BindProperty]
        public List<Casket> allcaskets { get; set; }
        [BindProperty]
        public OptionalImage Image { get; set; }
        [BindProperty]
        public UrnReusePhoto Photo { get; set; }

        [BindProperty]
        public FuneralPricing fp { get; set; }
        [BindProperty]
        public Boolean PhotoVal { get; set; }

        public IActionResult OnGet(string id)
        {
            // Validate if session exists
            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                // Testing Script
                HttpContext.Session.SetInt32("SSId", 1);
                //return NotFound();
            }

            int x = 0;
            Int32.TryParse(id, out x);
            Funeral = _svc.GetFuneralByFuneralId(x);

            if (Funeral == null)
            {
                return NotFound();
            }

            // Custom validation bypass
            if (Funeral.LocationAttire == "SampleText")
            {
                Funeral.LocationAttire = "";
            }
            if (Funeral.PlaqueName == "SampleText")
                Funeral.PlaqueName = "";

            if (string.IsNullOrEmpty(Funeral.Religion))
            {
                return Redirect("/prefuneral/religion?id=" + Funeral.Id);
            }
            if (Funeral.WakePostalCode == "999999")
            {
                return Redirect("/prefuneral/funeral-confirm?id=" + Funeral.Id);
            }

            fp = new FuneralPricing();
            allcaskets = _csvc.GetAllCaskets();
            return Page();
        }

        public IActionResult OnPost(OptionalImage Image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                return NotFound();
            }
            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("SSId");
            if (Photo.isReused == true)
            {
                Funeral.PlaquePhoto = Funeral.PhotoFramed;
            }
            else
            {
                if (Image.Image != null)
                {
                    PhotoVal = true;
                    return Page();
                }
                    
                string plaquephoto = UploadedFile(Image);
                Funeral.PlaquePhoto = plaquephoto;
            }


            var included = new[] { "FinalRestingPlace", "ColumbariumName", "PlaquePhoto", "PlaqueName", "PlaqueHasBday", "PlaqueHasDday", "PlaqueQuotes", "UrnId" };

            if (_svc.UpdateFuneral(Funeral, included) == true)
            {
                return Redirect("/prefuneral/plan-executor?id=" + Funeral.Id);
            }
            else
                return BadRequest();
        }

        private string UploadedFile(OptionalImage Image)
        {
            string uniqueFileName = null;

            if (Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/uploads/funeral_photoFramed/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
