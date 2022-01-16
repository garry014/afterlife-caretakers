using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class CasketService
    {
        private Models.ALCDBContext _context;
        public CasketService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddCasket(Casket newcasket)
        {
            newcasket.IsDeleted = false;
            newcasket.SelectedTimes = 0;
            if (CasketExists(newcasket.Id))
            {
                return false;
            }
            _context.Add(newcasket);
            _context.SaveChanges();
            return true;
        }
        public List<Casket> GetAllCaskets()
        {
            List<Casket> AllCaskets = new List<Casket>();
            AllCaskets = _context.Caskets.ToList();
            return AllCaskets;
        }
        public Casket GetCasketById(int id)
        {
            Casket theCasket = _context.Caskets.Where(c => c.Id == id).FirstOrDefault();
            return theCasket;
        }
        private bool CasketExists(int id)
        {
            return _context.Caskets.Any(c => c.Id == id);
        }

        public bool UpdateCasket(Casket thecasket)
        {
            bool updated = true;
            _context.Attach(thecasket).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasketExists(thecasket.Id))
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

        // Soft delete
        public bool DeleteCasket(Casket thecasket)
        {
            bool updated = true;
            _context.Attach(thecasket).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasketExists(thecasket.Id))
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
