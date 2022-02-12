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

namespace afterlife_caretakers.Pages.Admin
{
    public class AdminCreationModel : PageModel
    {
        private readonly Services.AdminService _svc;

        public AdminCreationModel(Services.AdminService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;
        
        [BindProperty]
        public Admins MyAdmin { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            MyAdmin.creationuser = "Admin1";
            int salt = 12;
            MyAdmin.password = BCrypt.Net.BCrypt.HashPassword(MyAdmin.password, salt);
            if (ModelState.IsValid)
            {
                if (_svc.AddAdminUsers(MyAdmin))
                {
                    // Create session

                    HttpContext.Session.SetString("SSName", MyAdmin.name);
                    HttpContext.Session.SetString("SSDept", MyAdmin.email);
                    return RedirectToPage("../Admin/Confirm_CreationAdmin");
                }
                else
                {
                    MyMessage = "Email has been registered!";
                    return Page();
                }
            }
            return Page();
        }
    }
}
