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
    public class start_planningModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public start_planningModel(Services.FuneralService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Funeral Funeral { get; set; }
        
        public void OnGet()
        {
            HttpContext.Session.SetInt32("SSId", 3);
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetInt32("SSId") != null)
            {
                Funeral funeral = _svc.GetFuneralByUserId((int)HttpContext.Session.GetInt32("SSId"));
                if (funeral != null)
                {
                    return Redirect("/prefuneral/Religion?id=" + funeral.Id);
                }
                else
                {
                    Boolean flag = _svc.AddFuneral(Funeral, (int)HttpContext.Session.GetInt32("SSId"));
                    return Redirect("/prefuneral/Religion?id=" + Funeral.Id);
                }
            }
            return Page();
        }
    }
}
