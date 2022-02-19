using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;

namespace afterlife_caretakers.Services
{

    public class WillService
    {
        public static PersonalInformation PersonalInfo;
        //public static PersonalInformation MaritalInfo;
        public static List<BeneficiaryInformation> beneList;
        //public static BeneficiaryInformation BeneInfo;
        //public static List<PersonalInformation> BeneficiaryInfo;

        private Models.ALCDBContext _context;
        public WillService(Models.ALCDBContext context)
        {
            _context = context;
        }
        //start of fiance operations , missing update cant dlt cuz u only have 1 so logically only update.
        public bool AddFiance(MaritalInfo newfiance)
        {
            newfiance.Mstatus = "Single";
            if (FianceExists(newfiance.Id))
            {
                return false;
            }
            _context.Add(newfiance);
            _context.SaveChanges();
            return true;
        }
        public MaritalInfo GetFianceById(int id)
        {
            MaritalInfo theFiance = _context.Fiance.Where(f => f.OWNERID == id).FirstOrDefault();
            return theFiance;
        }

        public List<MaritalInfo> GetFianceByOwnerId(int ownerid)
        {
            return _context.Fiance.Where(f => f.OWNERID == ownerid).ToList();
        }
        private bool FianceExists(int id)
        {
            return _context.Fiance.Any(f => f.Id == id);
        }
        public bool UpdateFiance(MaritalInfo thefiance)
        {
            bool updated = true;
            _context.Attach(thefiance).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FianceExists(thefiance.Id))
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
        //end of marital status


        //adding beneficiary
        public bool AddBeneficiary(BeneficiaryInformation newBeneficiary)

        {
            if (BeneficiaryExist(newBeneficiary.Id))
            {
                return false;
            }
            _context.Add(newBeneficiary);
            _context.SaveChanges();
            return true;
        }
        public List<BeneficiaryInformation> GetAllBeneficiary(int id)
        {
            List<BeneficiaryInformation> AllBeneficiary = new List<BeneficiaryInformation>();
            AllBeneficiary = _context.Beneficiary.ToList();
            return AllBeneficiary;
        }

        public List<BeneficiaryInformation> GetBeneficiaryFromOwner(int ownerid)
        {
            return _context.Beneficiary.Where(b => b.OWNERID == ownerid).ToList();
        }

        public BeneficiaryInformation GetBeneficiaryId(int id)
        {
            BeneficiaryInformation theBeneficiary = _context.Beneficiary.Where(b => b.Id == id).FirstOrDefault();
            return theBeneficiary;
        }
        public bool BeneficiaryExist(int id)
        {
            // with reference to dbset<>, latter var
            return _context.Beneficiary.Any(b => b.Id == id);
        }
        public bool UpdateBeneficiary(BeneficiaryInformation thebeneficiary)
        {
            bool updated = true;
            _context.Attach(thebeneficiary).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiaryExist(thebeneficiary.Id))
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
        public bool DeleteBeneficiary(BeneficiaryInformation thebeneficiary)
        {
            System.Diagnostics.Debug.WriteLine(thebeneficiary.Id);
            try
            {
                var beneRemove = _context.Beneficiary.SingleOrDefault(b => b.Id == thebeneficiary.Id);

                if (beneRemove == null)
                {
                    return false;
                }
                _context.Beneficiary.Remove(beneRemove);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public bool DeleteBeneficiary(BeneficiaryInformation thebenificiary)
        //{
        //    try
        //    {
        //        _context.Beneficiary.RemoveRange(_context.Beneficiary.Where(x => x.Id == thebenificiary.Id));
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        //end of operations for beneficiary

        //start of gift operations
        public bool AddAsset(Gift newasset)
        {
            //newasset.TYPE = "specific";
            //newasset.Mstatus = 0;
            if (AssetExists(newasset.Id))
            {
                return false;
            }
            _context.Add(newasset);
            _context.SaveChanges();
            return true;
        }
        public Gift GetAssetById(int id)
        {
            Gift theAsset = _context.Asset.Where(a => a.Id == id).FirstOrDefault();
            return theAsset;
        }
        public List<Gift> GetAllGift(int id)
        {
            List<Gift> AllGift = new List<Gift>();
            AllGift = _context.Asset.ToList();
            return AllGift;
        }

        public List<Gift> GetGiftFromOwner(int ownerid)
        {
            return _context.Asset.Where(a => a.OWNERID == ownerid).ToList();
        }
        private bool AssetExists(int id)
        {
            return _context.Asset.Any(a => a.Id == id);
        }
        public bool DeleteSingleAsset(Gift thegift)
        {
            try
            {
                var giftRemove = _context.Asset.SingleOrDefault(b => b.Id == thegift.Id);

                if (giftRemove == null)
                {
                    return false;
                }
                _context.Asset.Remove(giftRemove);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteAsset(int id)
        {
            try
            {
                _context.Asset.RemoveRange(_context.Asset.Where(x => x.BeneID == id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateAsset(Gift theasset)
        {
            bool updated = true;
            System.Diagnostics.Debug.WriteLine(theasset.Id);

            _context.Attach(theasset).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(theasset.Id))
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
        //end of gift 

        //start of executor operations
        public bool AddExecutor(ExecutorInformation newExecutor)

        {
            if (ExecutorExist(newExecutor.Id))
            {
                return false;
            }
            _context.Add(newExecutor);
            _context.SaveChanges();
            return true;
        }
        public ExecutorInformation GetExecutorById(int id)
        {
            ExecutorInformation theExecutor = _context.Executor.Where(e => e.Id == id).FirstOrDefault();
            return theExecutor;
        }
        public bool ExecutorExist(int id)
        {
            // with reference to dbset<>, latter var
            return _context.Executor.Any(e => e.Id == id);
        }
        //public List<ExecutorInformation> GetAllExecutor(int id)
        //{
        //    List<ExecutorInformation> AllExecutor = new List<ExecutorInformation>();
        //    AllExecutor = _context.Executor.ToList();
        //    return AllExecutor;
        //}
        public List<ExecutorInformation> GetAllExecutor(int ownerid)
        {
            return _context.Executor.Where(b => b.OWNERID == ownerid).ToList();
        }
        public List<ExecutorInformation> GetExecutorFromOwner(int ownerid)
        {
            return _context.Executor.Where(b => b.OWNERID == ownerid).ToList();
        }

        public bool UpdateExecutor(ExecutorInformation theexecutor)
        {
            bool updated = true;
            _context.Attach(theexecutor).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExecutorExist(theexecutor.Id))
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
        public bool DeleteExecutor(ExecutorInformation theexecutor)
        {
            try
            {
                var execRemove = _context.Executor.SingleOrDefault(b => b.Id == theexecutor.Id);

                if (execRemove == null)
                {
                    return false;
                }
                _context.Executor.Remove(execRemove);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        // end of executor operation

        //start of witness operation
        public bool AddWitness(WitnessInformation newWitness)

        {
            if (WitnessExist(newWitness.Id))
            {
                return false;
            }
            _context.Add(newWitness);
            _context.SaveChanges();
            return true;
        }
        public WitnessInformation GetWitnessById(int id)
        {
            WitnessInformation theWitness = _context.Witness.Where(w => w.Id == id).FirstOrDefault();
            return theWitness;
        }
        //public List<WitnessInformation> GetAllWitness(int id)
        //{
        //    List<WitnessInformation> AllWitness = new List<WitnessInformation>();
        //    AllWitness = _context.Witness.ToList();
        //    return AllWitness;
        //}
        public List<WitnessInformation> GetAllWitness(int ownerid)
        {
            return _context.Witness.Where(b => b.OWNERID == ownerid).ToList();
        }
        public List<WitnessInformation> GetWitnessFromOwner(int ownerid)
        {
            return _context.Witness.Where(w => w.OWNERID == ownerid).ToList();
        }
        public bool WitnessExist(int id)
        {
            return _context.Witness.Any(w => w.Id == id);
        }
        public bool UpdateWitness(WitnessInformation thewitness)
        {
            bool updated = true;
            _context.Attach(thewitness).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExecutorExist(thewitness.Id))
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
        public bool DeleteWitness(WitnessInformation thewitness)
        {
            try
            {
                var witRemove = _context.Witness.SingleOrDefault(b => b.Id == thewitness.Id);

                if (witRemove == null)
                {
                    return false;
                }
                _context.Witness.Remove(witRemove);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //delete operation 
        //end of witness operation
    }
}
