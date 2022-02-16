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
    public class EditCasketModel : PageModel
    {
        private readonly Services.CasketService _svc;
        public EditCasketModel(Services.CasketService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Casket Casket { get; set; }
        [BindProperty]
        public ImageClass Image { get; set; }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("admin_type") == null)
            {
                return NotFound();
            }
            if (!HttpContext.Session.GetString("admin_type").Contains("General_Admin"))
            {
                return NotFound();
            }
            
            if (id == null)
            {
                return NotFound();
            }

            int x = 0;
            Int32.TryParse(id, out x);
            Casket = _svc.GetCasketById(x);
            if (Casket == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(ImageClass Image)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = Image as IFormFile;
            if (file != null)
            {
                string uniqueFileName = UploadedFile(Image);
                Casket.ImageLink = uniqueFileName;
            }

            if (_svc.UpdateCasket(Casket) == true)
            {
                return RedirectToPage("/prefuneral/viewcasket");
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

                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/funeral/catalouge/");
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
