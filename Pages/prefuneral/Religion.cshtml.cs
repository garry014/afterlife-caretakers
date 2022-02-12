using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class ReligionModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public ReligionModel(Services.FuneralService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Funeral Funeral { get; set; }

        
        [BindProperty]
        public FuneralPricing fp { get; set; }

        public IActionResult OnGet(string id)
        {
            // Validate if session exists
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            int x = 0;
            Int32.TryParse(id, out x);
            Funeral = _svc.GetFuneralByFuneralId(x);

            if (Funeral == null)
            {
                return NotFound();
            }

            fp = new FuneralPricing();

            if (Funeral.Religion == "SampleText")
            {
                Funeral.Religion = "";
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Funeral.RequireRites && Funeral.ConductOptions == "")
            {
                new ValidationResult("Hello");
                return Page();
            }

            if (Funeral.RequireRites && Funeral.ConductOptions == "personal" && Funeral.ReligiousPName == "")
            {
                new ValidationResult("Hello");
                return Page();
            }

            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }
            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("user_id");

            var included = new[] { "Religion", "RequireRites", "ConductOptions", "ReligiousPName", "ReligiousPOCName", "ReligiousPOCNumber" };
            if (_svc.UpdateFuneral(Funeral, included) == true)
            {
                return Redirect("/prefuneral/wake-location?id=" + Funeral.Id);
            }
            else
                return BadRequest();
        }
    }
}
