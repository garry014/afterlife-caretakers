using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.appointment
{
    public class paymentapptModel : PageModel
    {
        private readonly Services.BookingService _bsvc;
        //private readonly Services.ConsultService _csvc;
        private readonly Services.PaymentService _psvc; // add this line in
        public paymentapptModel(Services.BookingService bservice, Services.PaymentService pservice)
        {
            _bsvc = bservice;
            //_csvc = cservice;
            _psvc = pservice; // Add this line in with Services.PaymentService pservice

        }
        //public FExecutorPermission FExecutor { get; set; }
        //[BindProperty]
        //public Funeral Funeral { get; set; }
        //[BindProperty]
        //public List<Casket> allcaskets { get; set; }
        //[BindProperty]
        //public FuneralPricing fp { get; set; }

        [BindProperty]
        public BookAppointment Appt { get; set; }
        [BindProperty]
        public double totalSum { get; set; }
        public double totalSumFinal;
        public int time { get; set; }

        //make sure open your db here, so u can pass in the $$ to paypal later.
        public IActionResult OnGet(int id)
        {
            // Validate if session exists, not logged in 
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }


            Appt = _bsvc.GetApptByConId(id);
            System.Diagnostics.Debug.WriteLine("conid: " + id);
            System.Diagnostics.Debug.WriteLine("apptconid: " + Appt.Id + " " + Appt.ApptType + " " + Appt.ConsultName);

            time = Appt.StartTime + (Appt.Duration * 100);

            totalSum = Appt.ConsultRate * Appt.Duration;

            

            //Counselling appointment with sarah lim (Rate)
            // date from time to time
            //show price 

            //Appt = _bsvc.GetApptByConId(conid);//get consult id from param


            //System.Diagnostics.Debug.WriteLine("Consult: " + Appt.Id + " " + Appt.ConsultId + " " + Appt.ConsultRate + " " + Appt.Duration);

            //int x = 0;
            //Int32.TryParse(id, out x);
            //Appt = _bsvc.GetApptById(x);

            //if (Appt == null)
            //{
            //    return NotFound();
            //}

            //from appt booking to payment, passes consult id in para, when come to payment, use same id 


            //fp = new FuneralPricing();
            //totalSum = fp.CalculateTotal(Funeral) - Funeral.PaymentAmount;
            //System.Diagnostics.Debug.WriteLine("thdsif");

            //allcaskets = _csvc.GetAllCaskets();

            //// execution rights
            //if (Funeral.WillMaker_ID != HttpContext.Session.GetInt32("user_id"))
            //{
            //    if (_fsvc.PermissionMappingExists((int)HttpContext.Session.GetInt32("user_id"), x))
            //    {
            //        return Page();
            //    }
            //    return NotFound();
            //}
            return Page();
        }


        // Copy this to your code
        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine("1postpay");
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid)
            //{
                //System.Diagnostics.Debug.WriteLine("2postpay");
                //int id = 0;
                //id = (int)HttpContext.Session.GetInt32("ApptId");
                //System.Diagnostics.Debug.WriteLine(" isit here" + id);
                //Appt = _bsvc.GetApptById(id);
                //// Create session
                return Redirect("/appointment/viewmyappt" );
            //return Redirect("/appointment/cfmpayment/"+ );
            //}

            //////////////////////////
      

        }
    }
}
