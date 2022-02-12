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
        private readonly Services.PaymentService _psvc; // add this line in
        public funeral_confirmModel(Services.FuneralService service, Services.CasketService cservice, Services.PaymentService pservice)
        {
            _svc = service;
            _csvc = cservice;
            _psvc = pservice; // Add this line in with Services.PaymentService pservice
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

        // make sure open your db here, so u can pass in the $$ to paypal later.
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
            totalSum = fp.CalculateTotal(Funeral) - Funeral.PaymentAmount;
            
            allcaskets = _csvc.GetAllCaskets();

            return Page();
        }


        // Copy this to your code
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
            fp = new FuneralPricing();
            totalSum = fp.CalculateTotal(Funeral) - Funeral.PaymentAmount;

            Payment Payment = new Payment();
            Payment.Item = "Pre-funeral planning"; // Change this to your own item name
            Payment.Price = totalSum; // Change this to your own pricing
            Payment.UserID = (int)HttpContext.Session.GetInt32("user_id");

            bool AddPaymentSuccess = _psvc.AddPayment(Payment);
            //if you dont need additional processing on your table
            //if (AddPaymentSuccess == true)
            //{
            //    return Redirect("");
            //}
            //else
            //{
            //    return BadRequest();
            //}

            // If you want to update your personal table
            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("user_id");
            Funeral.PaymentAmount = totalSum;
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
