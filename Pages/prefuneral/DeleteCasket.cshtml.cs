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
    public class DeleteCasketModel : PageModel
    {
        private readonly Services.CasketService _svc;
        public DeleteCasketModel(Services.CasketService service, IWebHostEnvironment hostEnvironment)
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
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Casket.IsDeleted = true;
            if (_svc.UpdateCasket(Casket) == true)
            {
                return RedirectToPage("/prefuneral/viewcasket");
            }
            else
                return BadRequest();

            return Page();
        }
    }
}
