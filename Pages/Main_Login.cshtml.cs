using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages
{
    public class Main_LoginModel : PageModel
    {
        private readonly Services.UserService _svc;

        //public Main_LoginModel(Services.UserService service)
        //{
        //    _svc = service;
        //}
        [BindProperty]
        public Users MyUser { get; set; }

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string name { get; set; }
        public string Msg;
        private ALCDBContext db;

        public Main_LoginModel(ALCDBContext _db)
        {
            db = _db;
        }
        

       
        
        public void OnGet()
        {
            MyUser = new Users();
        }
        
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("user_id");
            HttpContext.Session.Clear();
            return Page();
        }

        public IActionResult OnPost()
        {
            var acc = login(MyUser.email, MyUser.password);
            if(acc == null)
            {
                Msg = "Invalid";
                return Page();

            }
            else
            {
                HttpContext.Session.SetString("name", acc.name);
                HttpContext.Session.SetInt32("user_id", acc.Id);
                HttpContext.Session.SetString("usertype", acc.usertype);
                HttpContext.Session.SetString("user_email", acc.email);
                Console.WriteLine("Login with: " + acc.Id);
                return RedirectToPage("Index");
            }
            return Page();
        }

        private Users login(string email, string password)
        {
            Console.WriteLine(email);
            
            var account = db.users.FirstOrDefault(a => a.email.Equals(email));
            if(account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.password))
                {
                    return account;
                }
                return null;
            }
            return null;
        }
       
    }
}
