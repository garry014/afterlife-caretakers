using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using afterlife_caretakers.Models;

namespace afterlife_caretakers.Services
{
    public class AMDService
    {
        private ALCDBContext _context;
        public AMDService(ALCDBContext context)
        {
            _context = context;
        }
        public bool AddAMD(AMDWitness newamd)
        {
            if (AMDExists(newamd.Id))
            {
                return false;
            }
            // else if not exist, add employee, return true
            _context.Add(newamd);
            _context.SaveChanges();
            return true;
        }
        public List<AMDWitness> GetAllWitness()
        {
            List<AMDWitness> AllWitness = new List<AMDWitness>();
            AllWitness = _context.amdwitness.ToList();
            return AllWitness;
        }
        public AMDWitness GetWitnessById(int id)
        {
            List<AMDWitness> AllWitness = new List<AMDWitness>();
            AMDWitness witness = null;
            foreach (AMDWitness item in AllWitness)
            {
                if (item.Id == id)
                {
                    witness = item;
                }
            }
            AMDWitness theWitness = _context.amdwitness.Where(e => e.Id == id).FirstOrDefault();
            return theWitness;
        }
        private bool AMDExists(int id)
        {
            return _context.amdwitness.Any(e => e.Id == id);
        }
        public bool UpdateAMD(AMDWitness theWitness)
        {
            bool updated = true;
            _context.Attach(theWitness).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AMDExists(theWitness.Id))
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
