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

namespace afterlife_caretakers.Pages.prefuneral
{
    public class wake_plansModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        private readonly Services.CasketService _csvc;
        public wake_plansModel(Services.FuneralService service, Services.CasketService cservice, IWebHostEnvironment hostEnvironment)
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
        public ImageClass Image { get; set; }

        // Custom Validation
        [BindProperty]
        public Boolean PhotoFrameVal { get; set; }
        [BindProperty]
        public Boolean PhotoAttireVal { get; set; }

        [BindProperty]
        public FuneralPricing fp { get; set; }

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

            if (string.IsNullOrEmpty(Funeral.Religion))
            {
                return Redirect("/prefuneral/religion?id=" + Funeral.Id);
            }
            if (Funeral.WakePostalCode == "999999")
            {
                return Redirect("/prefuneral/wake-location?id=" + Funeral.Id);
            }
            fp = new FuneralPricing();
            allcaskets = _csvc.GetAllCaskets();
            return Page();
        }

        public IActionResult OnPost(ImageClass Image)
        {
            // try if this part still have any issues, should be fixed.
            Boolean valPassed = true;
            if (!ModelState.IsValid)
            {
                valPassed = false;
            }

            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                return NotFound();
            }

            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("SSId");

            // Custom Validation
            string photoframed = "";
            if (string.IsNullOrEmpty(Funeral.PhotoFramed))
            {
                if(Image.Image != null)
                {
                    photoframed = UploadedFile(Image);
                    Funeral.PhotoFramed = photoframed;
                }
                else
                {
                    PhotoFrameVal = true;
                    valPassed = false;
                }
            }
            else
            {
                PhotoFrameVal = true;
                if (Image.Image != null)
                {
                    photoframed = UploadedFile(Image);
                    Funeral.PhotoFramed = photoframed;
                }
            }

            string photoattire = "";
            if (string.IsNullOrEmpty(Funeral.PhotoAttire))
            {
                if (Image.Image2 != null)
                {
                    photoattire = UploadedFile2(Image);
                    Funeral.PhotoAttire = photoattire;
                }
                else
                {
                    PhotoAttireVal = true;
                    valPassed = false;
                }
                
            }
            else
            {
                PhotoAttireVal = true;
                if (Image.Image2 != null)
                {
                    photoattire = UploadedFile2(Image);
                    Funeral.PhotoAttire = photoattire;
                }
            }

            if (valPassed == false)
            {
                allcaskets = _csvc.GetAllCaskets();
                return Page();
            }
                

            List<string> listIncluded = new List<string>();
            listIncluded.Add("CasketID");
            listIncluded.Add("WakeDuration");
            listIncluded.Add("LocationAttire");
            listIncluded.Add("WakeGuestsExpected");
            if (!string.IsNullOrEmpty(photoframed))
            {
                listIncluded.Add("PhotoFramed");
            }
            if (!string.IsNullOrEmpty(photoattire))
            {
                listIncluded.Add("PhotoAttire");
            }
            string[] included = listIncluded.ToArray();

            if (_svc.UpdateFuneral(Funeral, included) == true)
            {
                return Redirect("/prefuneral/wake-addons?id=" + Funeral.Id);
            }
            else
                return BadRequest();
        }

        private string UploadedFile(ImageClass Image)
        {
            string uniqueFileName = null;

            if (Image != null)
            {
                // check if file is an image file
                var _extensions = new string[] { ".jpg", ".png" };
                var extension = Path.GetExtension(Image.Image.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return uniqueFileName;
                }

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

        private string UploadedFile2(ImageClass Image)
        {
            string uniqueFileName = null;

            if (Image != null)
            {
                // check if file is an image file
                var _extensions = new string[] { ".jpg", ".png" };
                var extension = Path.GetExtension(Image.Image.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return uniqueFileName;
                }

                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/uploads/funeral_photoAttire");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.Image2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.Image2.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
