using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.willmaking
{
    public class PaymentModel : PageModel
    {
        private readonly Services.PaymentService _psvc; // add this line in
        public PaymentModel(Services.PaymentService pservice)
        {
            _psvc = pservice; // Add this line in with Services.PaymentService pservice
        }
        public double totalSum { get; set; }
        public double totalSumFinal;


        public IActionResult OnGet(int id)
        {
            // Validate if session exists
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "WillMaker")
            {
                return Page();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //if (HttpContext.Session.GetInt32("user_id") == null)
            //{
            //    return NotFound();
            //}
            Users user = new Users();
            Payment Payment = new Payment();
            Payment.Item = "Will Creation"; // Change this to your own item name
            Payment.Price = 86; // Change this to your own pricing
            //Payment.UserID = (int)HttpContext.Session.GetInt32("user_id");
            Payment.FormId = 1;

            bool AddPaymentSuccess = _psvc.AddPayment(Payment);
            //if you dont need additional processing on your table
            if (AddPaymentSuccess == true)
            {
                user.willformID = "1";
                
                return Redirect("DownloadWill");
            }
            else
            {
                user.willformID = "";
                Payment.FormId = 0;
                return BadRequest();
            }
        }

        // If you want to update your personal table
        //Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("user_id");
        //Funeral.PaymentAmount = totalSum;
        //var included = new[] { "PaymentAmount" };
        //if (_svc.UpdateFuneral(Funeral, included) == true)
        //{
        //    return Redirect("/prefuneral/funeral-complete");
        //}
    }
}