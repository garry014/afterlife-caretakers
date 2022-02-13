using afterlife_caretakers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class BookingService
    {
        private Models.ALCDBContext _context;


        public BookingService(Models.ALCDBContext context)
        {
            _context = context;
        }
        public bool AddAppt(BookAppointment newappt)
        {
            if (ApptExists(newappt.Id))
            {
                return false;
            }
            _context.Add(newappt);
            _context.SaveChanges();
            return true;
        }
        private bool ApptExists(int id)
        {
            return _context.Appointments.Any(a => a.Id == id);
        }

        public BookAppointment GetApptById(int id)
        {

            BookAppointment theappt = _context.Appointments.Where(a => a.Id == id).FirstOrDefault();
            return theappt;
        }

        public BookAppointment GetApptByCustId(int custid)
        {

            BookAppointment theappt = _context.Appointments.Where(a => a.CustId == custid).FirstOrDefault();
            return theappt;
        }

        public BookAppointment GetApptByConId(int conid)
        {

            BookAppointment theconappt = _context.Appointments.Where(a => a.ConsultId == conid).FirstOrDefault();
            return theconappt;
        }
        public List<BookAppointment> GetAllAppt()
        {
            List<BookAppointment> AllAppt = new List<BookAppointment>();
            AllAppt = _context.Appointments.ToList();
            return AllAppt;
        }
    }
}
