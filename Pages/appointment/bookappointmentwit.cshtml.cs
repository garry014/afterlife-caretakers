using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;

namespace afterlife_caretakers.Pages.appointment
{
    public class bookappointmentwitModel : PageModel
    {
        private readonly Services.WitnessService _svc;
        private readonly Services.WitBooking _bsvc;
        private readonly Services.UserService _usvc;
        public bookappointmentwitModel(Services.WitnessService service, Services.WitBooking bservice, Services.UserService uservice)
        {
            _svc = service;
            _bsvc = bservice;
            _usvc = uservice;
        }

        [BindProperty]
        public WitAppointment ApptWit { get; set; }

        [BindProperty]
        public WitnessConsult Witness { get; set; }

        [BindProperty]
        public Users User { get; set; }

        [BindProperty]
        public List<WitnessConsult> allconsults { get; set; }

        [BindProperty]
        public string ValidateMsg { get; set; }
        [BindProperty]
        public List<int> TimeSlots { get; set; }
        [BindProperty]
        public DateTime ApptDate { get; set; }
        public List<WitAppointment> ApptList { get; set; }
        [BindProperty]
        public List<int> timeslot { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                // return NotFound(); //not loggeed in 
                return RedirectToPage("/Main_Login");
            }

            System.Diagnostics.Debug.WriteLine("thdsif");
            List<WitAppointment> apptlist = _bsvc.GetAllAppt();
            int id = 0;
            int userid = 0;
            if (HttpContext.Session.GetInt32("ApptId") == null)
            {
                return NotFound();
            }

            id = (int)HttpContext.Session.GetInt32("ApptId");
            userid = (int)HttpContext.Session.GetInt32("user_id");
            System.Diagnostics.Debug.WriteLine(id);
            if (id != 0)
            {
                User = _usvc.GetUserByID(userid);
                System.Diagnostics.Debug.WriteLine(User.name);
                Witness = _svc.GetWitConsultById(id);
                ApptDate = DateTime.Parse(HttpContext.Session.GetString("ApptDate"));
                ApptList = _bsvc.GetAllAppt();
                //ApptDate.ToString("dd-MM-yyyy")
                timeslot = new List<int>();
                for (int i = 800; i < 2100; i += 100)
                {
                    timeslot.Add(i);
                }
                System.Diagnostics.Debug.WriteLine(timeslot.ToString());
                foreach (var i in ApptList)
                {
                    System.Diagnostics.Debug.WriteLine("Is it true:" + i.Date.ToString("dd-MM-yyyy") + ", " + ApptDate.ToString("dd-MM-yyyy"));
                    if (i.Date.ToString("dd-MM-yyyy") == ApptDate.ToString("dd-MM-yyyy"))
                    {
                        int endtime = i.StartTime + (i.Duration * 100);
                        
                        for (var t=0; t<timeslot.Count; t++)
                        {
                            System.Diagnostics.Debug.WriteLine(timeslot[t]);
                            if (timeslot[t] >= i.StartTime)
                            {
                                timeslot.RemoveRange(t, i.Duration);
                                System.Diagnostics.Debug.WriteLine("T:"+t);
                                break;
                            }
                        }
                    }
                }
                //ApptDate = ApptDate.ToShortDateStr111111ing();
                System.Diagnostics.Debug.WriteLine(ApptDate);
                return Page();
            }
            else
            {
                return RedirectToPage("/witness/teamwitnesses");
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
            System.Diagnostics.Debug.WriteLine("print here" + ApptWit.StartTime);
            ApptDate = DateTime.Parse(HttpContext.Session.GetString("ApptDate"));
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("nono");
                if (_bsvc.AddAppt(ApptWit))
                {
                    int id = 0;
                    id = (int)HttpContext.Session.GetInt32("ApptId");
                    System.Diagnostics.Debug.WriteLine(" ID here" + id);
                    Witness = _svc.GetWitConsultById(id);
                    // Create session
                    return Redirect("../Index"); //patrick and kj 
                }
            }
            return Page();
        }
    }
}
