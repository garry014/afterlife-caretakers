using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly Services.AdminService _svc;

        [BindProperty]
        public Admins MyAdmin { get; set; }

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string name { get; set; }

        public string admin_role { get; set; }
        public string Msg;
        private ALCDBContext db;

        public AdminLoginModel(ALCDBContext _db)
        {
            db = _db;
        }

        public void OnGet()
        {
            MyAdmin = new Admins();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("user_id");
            HttpContext.Session.Clear();
            return Page();
        }

        public IActionResult OnPost()
        {
            var acc = login(MyAdmin.email, MyAdmin.password);
            if (acc == null)
            {
                Msg = "Invalid";
                return Page();

            }
            else
            {
                HttpContext.Session.SetString("name", acc.name);
                HttpContext.Session.SetInt32("user_id", acc.Id);
                HttpContext.Session.SetString("admin_type", acc.admin_role);
                Console.WriteLine("Login with: " + acc.Id);
                Console.WriteLine("Login with: " + acc.admin_role);
                return RedirectToPage("../Admin/AdminHome");
            }
            return Page();
        }

        private Admins login(string email, string password)
        {
            Console.WriteLine(email);

            var account = db.admins.FirstOrDefault(a => a.email.Equals(email));
            if (account != null)
            {
                Console.WriteLine(password);
                Console.WriteLine(account.password);
                if (BCrypt.Net.BCrypt.Verify(password, account.password))
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


    }
}
