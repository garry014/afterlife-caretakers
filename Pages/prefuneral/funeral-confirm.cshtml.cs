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
    public class funeral_confirmModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        private readonly Services.CasketService _csvc;
        public funeral_confirmModel(Services.FuneralService service, Services.CasketService cservice)
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
        public double totalSumFinal;

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

            fp = new FuneralPricing();
            totalSum = fp.CalculateTotal(Funeral) - Funeral.PaymentAmount;
            
            allcaskets = _csvc.GetAllCaskets();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                return NotFound();
            }
            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("SSId");
            var included = new[] { "PaymentAmount" };
            if (_svc.UpdateFuneral(Funeral, included) == true)
            {
                return Redirect("/prefuneral/funeral-complete");
            }
            else
                return BadRequest();
        }

        
    }
}
