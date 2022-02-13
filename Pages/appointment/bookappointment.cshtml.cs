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
    public class bookappointmentModel : PageModel
    {
        private readonly Services.ConsultService _svc;
        private readonly Services.BookingService _bsvc;
        private readonly Services.UserService _usvc;
        public bookappointmentModel(Services.ConsultService service, Services.BookingService bservice, Services.UserService uservice)
        {
            _svc = service;
            _bsvc = bservice;
            _usvc = uservice;
        }

        [BindProperty]
        public BookAppointment Appt { get; set; }

        [BindProperty]
        public ConsultProfile Consult { get; set; }

        [BindProperty]
        public Users User { get; set; }

        [BindProperty]
        public List<ConsultProfile> allconsults { get; set; }

        [BindProperty]
        public string ValidateMsg { get; set; }
        [BindProperty]
        public List<int> TimeSlots { get; set; }
        [BindProperty]
        public DateTime ApptDate { get; set; }
        public List<BookAppointment> ApptList { get; set; }
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
            List<BookAppointment> apptlist = _bsvc.GetAllAppt();
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
                Consult = _svc.GetConsultById(id);
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
                        System.Diagnostics.Debug.WriteLine("endtime: " + endtime);
                        for (var t=0; t<timeslot.Count; t++)
                        {
                            System.Diagnostics.Debug.WriteLine("timeslot: " + timeslot[t]);
                            if (timeslot[t] >= i.StartTime)
                            {
                                timeslot.RemoveRange(t, i.Duration);
                                System.Diagnostics.Debug.WriteLine("Remove T:"+t);
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
            System.Diagnostics.Debug.WriteLine("print here" + Appt.StartTime);
            Appt.Date = DateTime.Parse(HttpContext.Session.GetString("ApptDate"));
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("nono");
                if (_bsvc.AddAppt(Appt))
                {
                    int id = 0;
                    id = (int)HttpContext.Session.GetInt32("ApptId");
                    System.Diagnostics.Debug.WriteLine(" ID here" + id);
                    Consult = _svc.GetConsultById(id);
                    // Create session
                    return Redirect("../appointment/paymentappt/"+ Consult.Id);
                }
            }
            return Page();
        }
    }
}
