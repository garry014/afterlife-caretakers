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
    public class apptdateModel : PageModel
    {
        private readonly Services.ConsultService _svc;
        private readonly Services.BookingService _bsvc;
        public apptdateModel(Services.ConsultService service, Services.BookingService bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }

        [BindProperty]
        public DateTime Appt { get; set; }
       

        [BindProperty]
        public ConsultProfile Consult { get; set; }

        [BindProperty]
        public List<ConsultProfile> allconsults { get; set; }

        [BindProperty]
        public string ValidateMsg { get; set; }
        [BindProperty]
        public List<int> TimeSlots { get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                // return NotFound(); //not loggeed in 
                return RedirectToPage("/Main_Login");
            }

            System.Diagnostics.Debug.WriteLine("thdsif");
            
            List<BookAppointment> apptlist = _bsvc.GetAllAppt();
            if (id != 0)
            {
                Consult = _svc.GetConsultById(id);
                HttpContext.Session.SetInt32("ApptId", id);
                return Page();
            }
            else
            {
                return RedirectToPage("/counsel/teamcounsellors");
            }

            //foreach (var i in apptlist)
            //{
            //    if(i.ConsultName == Consult.ConsultName)
            //    {
            //        if(i.Date == )
            //    }
            //}
        }

        public IActionResult OnPost()
        {
            //System.Diagnostics.Debug.WriteLine("print here" + Appt.StartTime);

            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("nono1111");
                
                if (Appt != null)
                {
                    // Create session
                    
                    HttpContext.Session.SetString("ApptDate", Appt.ToString());
                    System.Diagnostics.Debug.WriteLine("not null");
                    return RedirectToPage("/appointment/bookappointment");
                }
            }

            return Page();
        }
    }
}
