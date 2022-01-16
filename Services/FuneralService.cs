using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class FuneralService
    {
        private Models.ALCDBContext _context;
        public FuneralService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddFuneral(Funeral newfuneral, int willmaker_id)
        {
            // Init
            newfuneral.Religion = "SampleText";
            newfuneral.RequireRites = false;
            //newfuneral.ConductOptions = "SampleText";
            //newfuneral.ReligiousPName = "SampleText";
            newfuneral.WakeLocationIn = "SampleText";
            newfuneral.WakePostalCode = "999999";
            newfuneral.CasketID = 0;
            newfuneral.WakeDuration = 0;
            newfuneral.LocationAttire = "SampleText";
            newfuneral.WakeGuestsExpected = 0;
            newfuneral.FuneralVechicle = "SampleText";
            newfuneral.FGuestsExpected = 0;
            newfuneral.FinalRestingPlace = "SampleText";
            newfuneral.ColumbariumName = "SampleText";
            newfuneral.PlaqueName = "SampleText";
            newfuneral.UrnId = 0;
            newfuneral.PaymentAmount = 0;

            newfuneral.IsDeleted = false;
            newfuneral.WillMaker_ID = willmaker_id;

            DateTime date1 = DateTime.UtcNow;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            newfuneral.TimeStamp = TimeZoneInfo.ConvertTime(date1, tz);
            newfuneral.LastUpdatedTime = TimeZoneInfo.ConvertTime(date1, tz);
            if (WillMakerExists(willmaker_id))
            {
                return false;
            }
            _context.Add(newfuneral);
            _context.SaveChanges();
            return true;
        }
        public List<Funeral> GetAllFunerals()
        {
            List<Funeral> AllFunerals = new List<Funeral>();
            AllFunerals = _context.FuneralPlans.ToList();
            return AllFunerals;
        }

        // Get Funeral Plan by will maker id
        public Funeral GetFuneralByUserId(int id)
        {
            Funeral theFuneral = _context.FuneralPlans.Where(c => c.WillMaker_ID == id).FirstOrDefault();
            return theFuneral;
        }
        public Funeral GetFuneralByFuneralId(int id)
        {
            Funeral theFuneral = _context.FuneralPlans.Where(c => c.Id == id).FirstOrDefault();
            return theFuneral;
        }
        private bool FuneralExists(int id)
        {
            return _context.FuneralPlans.Any(c => c.Id == id);
        }

        public bool WillMakerExists(int id)
        {
            return _context.FuneralPlans.Any(w => w.WillMaker_ID == id);
        }

        public bool UpdateFuneral(Funeral thefuneral, string[] included)
        {
            //var included = new[] { "Religion", "RequireRites"

            bool updated = true;
            DateTime date1 = DateTime.UtcNow;
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            thefuneral.LastUpdatedTime = TimeZoneInfo.ConvertTime(date1, tz);

            var entry = _context.Entry(thefuneral);
            entry.State = EntityState.Unchanged;
            //_context.Attach(thefuneral).State = EntityState.Modified;
            foreach (var name in included)
            {
                entry.Property(name).IsModified = true;
            }
            entry.Property("LastUpdatedById").IsModified = true;
            entry.Property("LastUpdatedTime").IsModified = true;
            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuneralExists(thefuneral.Id))
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
        public bool DeleteCasket(Funeral thefuneral)
        {
            bool updated = true;
            _context.Attach(thefuneral).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuneralExists(thefuneral.Id))
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
