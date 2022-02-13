using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.team
{
    public class viewwitnessapptModel : PageModel
    {
        [BindProperty]
        public List<WitAppointment> allmyappt { get; set; }
        [BindProperty]
        public List<WitAppointment> consultpic { get; set; }

        [BindProperty]
        public WitnessConsult Witness { get; set; }

        [BindProperty]
        public WitAppointment WitAppt { get; set; }
        [BindProperty]
        public int Id { get; set; }

        private readonly Services.WitnessService _svc;
        private readonly Services.WitBooking _bsvc;
        public viewwitnessapptModel(Services.WitnessService service, Services.WitBooking bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }

        public IActionResult OnGet() //get user id? or dont need? 
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                // return NotFound(); //not loggeed in 
                return RedirectToPage("../Admin/AdminLogin");
            }
            //need get user id, compare if is in appt table, display all
            //customer service, get id 
            allmyappt = _bsvc.GetAllAppt();

            Id = (int)HttpContext.Session.GetInt32("user_id");
            Console.WriteLine(Id);
            return Page();


            //consultdetails = _svc.GetAllAppt();

        }
    }
}
