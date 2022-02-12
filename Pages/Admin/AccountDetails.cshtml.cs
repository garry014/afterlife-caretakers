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
    public class AccountDetailsModel : PageModel
    {
        private readonly Services.AdminService _svc;
        public AccountDetailsModel(Services.AdminService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Admins MyAdmin { get; set; }

        public IActionResult OnGet()
        {
            Console.WriteLine("wattttt");

            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            //var id = (int)HttpContext.Session.GetInt32("user_id");
            var current_user = (int)HttpContext.Session.GetInt32("user_id");
            Console.WriteLine("Edit with " + current_user);


            MyAdmin = _svc.GetAdminByID(current_user);

            if (MyAdmin == null)
            {
                Console.WriteLine("id found:" + MyAdmin.name);
                return NotFound();
            }


            return Page();
        }

        public IActionResult OnPost()
        {


            if (string.IsNullOrEmpty(MyAdmin.password))
            {
                MyAdmin.password = BCrypt.Net.BCrypt.HashPassword(MyAdmin.password);
            }

            Console.WriteLine("wattttt2");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("wattttt5");

                return Page();
            }

            if (_svc.UpdateAdmin(MyAdmin) == true)
            {
                Console.WriteLine("wattttt3");
                TempData["AlertMessage"] = "You have Updated Your Account Successfully!";
                return RedirectToPage("/Admin/AccountDetails");
            }
            else
                return BadRequest();
        }
    }
}
