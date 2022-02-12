using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class createcasketModel : PageModel
    {
        private readonly Services.CasketService _svc;
        public createcasketModel(Services.CasketService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Casket Casket { get; set; }
        
        [BindProperty]
        public ImageClass Image { get; set; }
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "Admin")
            {
                return Page();
            }
            return NotFound();
        }

        public IActionResult OnPost(ImageClass Image)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = "";
                var file = Image as IFormFile;
                if (file != null)
                {
                    uniqueFileName = UploadedFile(Image);
                }
                else
                {
                    return Page();
                }
                
                Casket.ImageLink = uniqueFileName;
                if (_svc.AddCasket(Casket))
                {
                    return RedirectToPage("/prefuneral/viewcasket");
                }
                
            }
            return Page();
        }

        private string UploadedFile(ImageClass Image)
        {
            string uniqueFileName = null;
            var file = Image as IFormFile;
            if (file != null)
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
