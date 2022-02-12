using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class viewplansModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        private readonly Services.CasketService _csvc;
        public viewplansModel(Services.FuneralService service, Services.CasketService cservice)
        {
            _svc = service;
            _csvc = cservice;
        }

        [BindProperty]
        public Funeral Funeral { get; set; }
        [BindProperty]
        public List<Casket> allcaskets { get; set; }
        [BindProperty]
        public FuneralPricing fp { get; set; }
        [BindProperty]
        public double totalSum { get; set; }

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
            totalSum = fp.CalculateTotal(Funeral);

            allcaskets = _csvc.GetAllCaskets();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
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
