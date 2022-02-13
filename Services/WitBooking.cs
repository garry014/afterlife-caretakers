using afterlife_caretakers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class WitBooking
    {
        private Models.ALCDBContext _context;


        public WitBooking(Models.ALCDBContext context)
        {
            _context = context;
        }
        public bool AddAppt(WitAppointment newappt)
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
            return _context.WitAppt.Any(a => a.Id == id);
        }

        public WitAppointment GetApptById(int id)
        {

            WitAppointment theappt = _context.WitAppt.Where(a => a.Id == id).FirstOrDefault();
            return theappt;
        }

        public WitAppointment GetApptByCustId(int custid)
        {

            WitAppointment theappt = _context.WitAppt.Where(a => a.CustId == custid).FirstOrDefault();
            return theappt;
        }

        public WitAppointment GetApptByConId(int conid)
        {

            WitAppointment theconappt = _context.WitAppt.Where(a => a.ConsultId == conid).FirstOrDefault();
            return theconappt;
        }
        public List<WitAppointment> GetAllAppt()
        {
            List<WitAppointment> AllAppt = new List<WitAppointment>();
            AllAppt = _context.WitAppt.ToList();
            return AllAppt;
        }
    }
}
