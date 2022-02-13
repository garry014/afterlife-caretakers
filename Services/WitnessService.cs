using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class WitnessService
    {
        private Models.ALCDBContext _context;

        public WitnessService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddWitConsult(WitnessConsult newconsult)
        {

            if (WitConsultUserExists(newconsult.UserId))
            {
                System.Diagnostics.Debug.WriteLine(newconsult.Id);
                System.Diagnostics.Debug.WriteLine(newconsult.UserId);
                System.Diagnostics.Debug.WriteLine("Userid alr exists");
                return false;
            }

            _context.Add(newconsult);
            _context.SaveChanges();
            return true;
        }
        public WitnessConsult GetWitConsultByUserId(int uid)
        {

            WitnessConsult theconsult = _context.WitnessConsults.Where(cp => cp.UserId == uid).FirstOrDefault();
            return theconsult;
        }

        public WitnessConsult GetWitConsultById(int id)
        {

            WitnessConsult theconsult = _context.WitnessConsults.Where(cp => cp.Id == id).FirstOrDefault();
            return theconsult;
        }
        private bool WitConsultExists(int id)
        {
            return _context.WitnessConsults.Any(cp => cp.Id == id);
        }
        private bool WitConsultUserExists(int userid)
        {
            return _context.WitnessConsults.Any(cp => cp.UserId == userid);
        }
        public List<WitnessConsult> GetAllWitConsults()
        {
            List<WitnessConsult> AllConsults = new List<WitnessConsult>();
            AllConsults = _context.WitnessConsults.ToList();
            return AllConsults;
        }



        public bool UpdateWitConsult(WitnessConsult theconsult)
        {
            bool updated = true;
            System.Diagnostics.Debug.WriteLine(theconsult.Id);
            _context.Attach(theconsult).State = EntityState.Modified;
            _context.Entry(theconsult).Property(x => x.Id).IsModified = false;

            try
            {

                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WitConsultExists(theconsult.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }
    }
}
