using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class plan_controlsModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public plan_controlsModel(Services.FuneralService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Funeral Funeral { get; set; }
        [BindProperty]
        public string errorMsg { get; set; }

        public IActionResult OnGet()
        {
            HttpContext.Session.SetInt32("SSId", 6);
            if (HttpContext.Session.GetInt32("SSId") != null)
            {
                Funeral = _svc.GetFuneralByUserId((int)HttpContext.Session.GetInt32("SSId"));
                if (Funeral == null)
                {
                    errorMsg = "You do not have a plan yet.";
                }
                return Page();
            }
            errorMsg = "Please login to view this page";
            return Page();
        }

        public IActionResult OnPost()
        {
            if (_svc.DeleteFuneral(Funeral))
            {
                return Redirect("/prefuneral/deleteplansuccess");
            }
            return Page();
        }
    }
}
