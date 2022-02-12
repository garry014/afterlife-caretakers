using afterlife_caretakers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class BVideoPermissionService
    {
        private Models.ALCDBContext _context;
        public BVideoPermissionService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddVideo(BVideoPermission newvideo)
        {
            //if (PermissionMappingExists(newpermission.executor_id, newpermission.funeral_id))
            //{
            //    return false;
            //}
            _context.Add(newvideo);
            _context.SaveChanges();
            return true;
        }

        // Remove multiple 
        public bool DeletePermissions(BVideoPermission permission)
        {
            try
            {
                _context.BVideoPermission.RemoveRange(_context.BVideoPermission.Where(x => x.video_id == permission.video_id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<BVideoPermission> GetAllPermissions()
        {
            List<BVideoPermission> AllPermissions = new List<BVideoPermission>();
            AllPermissions = _context.BVideoPermission.ToList();
            return AllPermissions;
        }
        public List<BVideoPermission> GetAllPermissionsByBId(string bene_id)
        {
            List<BVideoPermission> AllPermissions = new List<BVideoPermission>();
            AllPermissions = _context.BVideoPermission.Where(c => c.bene_id == bene_id).ToList();
            return AllPermissions;
        }
        //public BVideoPermission GetPermissionByBId(string bene_id)
        //{
        //    BVideoPermission thePermission = _context.BVideoPermission.Where(c => c.bene_id == bene_id).FirstOrDefault();
        //    return thePermission;
        //}
        
        public bool PermissionMappingExists(string bene_id, int video_id)
        {
            return _context.BVideoPermission.Any(c => c.bene_id == bene_id && c.video_id == video_id);
        }
    }
}
