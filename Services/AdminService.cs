using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace afterlife_caretakers.Services
{
    public class AdminService
    {
        private Models.ALCDBContext _context;

        public AdminService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddAdminUsers(Admins newadmin)
        {
            if (adminExists(newadmin.Id))
            {
                return false;
            }
            _context.Add(newadmin);
            _context.SaveChanges();
            return true;
        }

        public List<Admins> GetAllAdmin()
        {
            List<Admins> AllAdmin = new List<Admins>();
            AllAdmin = _context.admins.ToList();
            return AllAdmin;
        }

        public Admins GetAdminByID(int id)
        {
            //List<Users> AllUsers = new List<Users>();
            //Users user = null;
            //foreach (Users item in AllUsers)
            //{
            //    if (item.Id == id)
            //    {
            //        user = item;
            //        break;
            //    }
            //}
            Admins theAdmin = _context.admins.Where(e => e.Id == id).FirstOrDefault();
            return theAdmin;
        }

        private bool adminExists(int id)
        {
            return _context.admins.Any(e => e.Id == id);
        }

        public bool UpdateAdmin(Admins theadmin)

        {
            bool updated = true;
            _context.Attach(theadmin).State = EntityState.Modified;

            try
            {


                _context.SaveChanges();
                updated = true;


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adminExists(theadmin.Id))
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
