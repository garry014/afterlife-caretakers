using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class UserService
    {
        private Models.ALCDBContext _context;

        public UserService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddUsers(Users newusers)
        {
            if (userExists(newusers.Id))
            {
                return false;
            }
            _context.Add(newusers);
            _context.SaveChanges();
            return true;
        }

        public List<Users> GetAllUsers()
        {
            List<Users> AllUsers = new List<Users>();
            AllUsers = _context.users.ToList();
            return AllUsers;
        }

        public Users GetUserByID(int id)
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
            Users theUser = _context.users.Where(e => e.Id == id).FirstOrDefault();
            return theUser;
        }

        private bool userExists(int id)
        {
            return _context.users.Any(e => e.Id == id);
        }

        

        public bool UpdateUser(Users theuser)

        {
            bool updated = true;
            _context.Attach(theuser).State = EntityState.Modified;

            try
            {
               

                _context.SaveChanges();
                updated = true;


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(theuser.Id))
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

