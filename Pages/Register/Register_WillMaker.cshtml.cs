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

namespace afterlife_caretakers.Pages.Register
{
    public class Register_WillMakerModel : PageModel
    {
        private readonly Services.UserService _svc;
        public Register_WillMakerModel(Services.UserService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Users MyUsers { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        [BindProperty]
        public ImageClass Image { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            MyUsers.usertype = "WillMaker";
            MyUsers.activation_status = "inactive";
            int salt = 12;
            MyUsers.password = BCrypt.Net.BCrypt.HashPassword(MyUsers.password, salt);
            if (ModelState.IsValid)
            {
                if (_svc.AddUsers(MyUsers))
                {
                    // Create session
                    // what is this for???
                    HttpContext.Session.SetString("SSName", MyUsers.name);
                    HttpContext.Session.SetString("SSDept", MyUsers.email);
                    return RedirectToPage("/Confirm_Registration");
                }
                else
                {
                    //exception handling? 
                    MyMessage = "Email has been registered!";
                    return Page();
                }
            }
            return Page();
        }

        private string UploadedFile(ImageClass Image)
        {
            string uniqueFileName = null;

            if (Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/userphotos/");
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
