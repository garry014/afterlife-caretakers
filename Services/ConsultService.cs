using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class ConsultService
    {
        private Models.ALCDBContext _context;

        public ConsultService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddConsult(ConsultProfile newconsult)
        {
           
            if (ConsultUserExists(newconsult.UserId))
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
        public ConsultProfile GetConsultByUserId(int uid)
        {

            ConsultProfile theconsult = _context.ConsultProfiles.Where(cp => cp.UserId == uid).FirstOrDefault();
            return theconsult;
        }

        public ConsultProfile GetConsultById(int id)
        {

            ConsultProfile theconsult = _context.ConsultProfiles.Where(cp => cp.Id == id).FirstOrDefault();
            return theconsult;
        }
        private bool ConsultExists(int id)
        {
            return _context.ConsultProfiles.Any(cp => cp.Id == id);
        }
        private bool ConsultUserExists(int userid)
        {
            return _context.ConsultProfiles.Any(cp => cp.UserId == userid);
        }
        public List<ConsultProfile> GetAllConsults()
        {
            List<ConsultProfile> AllConsults = new List<ConsultProfile>();
            AllConsults = _context.ConsultProfiles.ToList();
            return AllConsults;
        }



        public bool UpdateConsult(ConsultProfile theconsult)
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
                if (!ConsultExists(theconsult.Id))
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
