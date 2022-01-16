using afterlife_caretakers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class FExecutorPermissionService
    {
        private Models.ALCDBContext _context;
        public FExecutorPermissionService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddCasket(FExecutorPermission newpermission)
        {
            if (PermissionMappingExists(newpermission.executor_id, newpermission.funeral_id))
            {
                return false;
            }
            _context.Add(newpermission);
            _context.SaveChanges();
            return true;
        }
        public List<FExecutorPermission> GetAllPermissions()
        {
            List<FExecutorPermission> AllPermissions = new List<FExecutorPermission>();
            AllPermissions = _context.FExecutorPermission.ToList();
            return AllPermissions;
        }
        public FExecutorPermission GetPermissionByFId(int funeral_id)
        {
            FExecutorPermission thePermission = _context.FExecutorPermission.Where(c => c.executor_id == funeral_id).FirstOrDefault();
            return thePermission;
        }
        private bool PermissionMappingExists(int executor_id, int funeral_id)
        {
            return _context.FExecutorPermission.Any(c => c.executor_id == executor_id && c.funeral_id == funeral_id);
        }

        //public bool UpdateCasket(FExecutorPermission thecasket)
        //{
        //    bool updated = true;
        //    _context.Attach(thecasket).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChanges();
        //        updated = true;

        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CasketExists(thecasket.Id))
        //        {
        //            updated = false;
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return updated;


        //}

        // Soft delete
        //public bool DeleteCasket(Casket thecasket)
        //{
        //    bool updated = true;
        //    _context.Attach(thecasket).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChanges();
        //        updated = true;

        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CasketExists(thecasket.Id))
        //        {
        //            updated = false;
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return updated;


        //}
    }
}
