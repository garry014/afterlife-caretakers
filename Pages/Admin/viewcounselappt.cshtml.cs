using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.Admin
{
    public class viewcounselapptModel : PageModel
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
        public viewcounselapptModel(Services.ConsultService service, Services.BookingService bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }

        public IActionResult OnGet() //get user id? or dont need? 
        {
            //need get user id, compare if is in appt table, display all
            //customer service, get id 
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                // return NotFound(); //not loggeed in 
                return RedirectToPage("../Main_Login");
            }
            allmyappt = _bsvc.GetAllAppt();
            //string conname = null;

            Id = (int)HttpContext.Session.GetInt32("user_id");
            Console.WriteLine(Id);

            //string conname = null;
            return Page();


            //consultdetails = _svc.GetAllAppt();

        }
    }
}
