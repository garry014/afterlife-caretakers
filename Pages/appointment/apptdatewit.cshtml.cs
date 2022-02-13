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
    public class apptdatewitModel : PageModel
    {
        private readonly Services.WitnessService _svc;
        private readonly Services.WitBooking _bsvc;
        public apptdatewitModel(Services.WitnessService service, Services.WitBooking bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }

        [BindProperty]
        public DateTime Appt { get; set; }


        [BindProperty]
        public WitnessConsult Witness { get; set; }

        [BindProperty]
        public List<WitnessConsult> allconsults { get; set; }

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
            List<WitAppointment> apptlist = _bsvc.GetAllAppt();
            if (id != 0)
            {
                Witness = _svc.GetWitConsultById(id);
                HttpContext.Session.SetInt32("ApptId", id);
                return Page();
            }
            else
            {
                return RedirectToPage("/witnesses/teamwitnesses");
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
                    return RedirectToPage("/appointment/bookappointmentwit");
                }
            }

            return Page();
        }
    }
}
