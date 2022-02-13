using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.Admin
{
    public class viewdoctorapptModel : PageModel
    {

        [BindProperty]
        public List<BookAppointment> allmyappt { get; set; }
        [BindProperty]
        public List<ConsultProfile> consultpic { get; set; }

        [BindProperty]
        public ConsultProfile Consult { get; set; }

        [BindProperty]
        public BookAppointment Apppointment { get; set; }
        [BindProperty]
        public int Id { get; set; }

        private readonly Services.ConsultService _svc;
        private readonly Services.BookingService _bsvc;
        public viewdoctorapptModel(Services.ConsultService service, Services.BookingService bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }

        public IActionResult OnGet() //get user id? or dont need? 
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return RedirectToPage("../Main_Login");
            }
            //need get user id, compare if is in appt table, display all
            //customer service, get id 
            allmyappt = _bsvc.GetAllAppt();

            Id = (int)HttpContext.Session.GetInt32("user_id");
            Console.WriteLine(Id);
            //var current_user = (int)HttpContext.Session.GetInt32("user_id");
            //System.Diagnostics.Debug.WriteLine("curr : " + current_user);
            //Consult = _svc.GetConsultByUserId(current_user);
            //System.Diagnostics.Debug.WriteLine("thisconsultid : " + Consult.UserId + "," + Consult.Id);
            //if (Consult.ServiceType != "Doctor")
            //{
            //    System.Diagnostics.Debug.WriteLine("gijh : " );
            //    return NotFound();
            //} 
            //string conname = null;

            return Page();
            //consultdetails = _svc.GetAllAppt();

        }
    }
}
