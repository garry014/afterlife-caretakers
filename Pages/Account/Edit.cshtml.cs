using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly Services.UserService _svc;
        public EditModel(Services.UserService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Users MyUser { get; set; }

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


            MyUser = _svc.GetUserByID(current_user);

            if (MyUser == null)
            {
                Console.WriteLine("id found:" + MyUser.name);
                return NotFound();
            }


            return Page();
        }

        public IActionResult OnPost()
        {


            if (string.IsNullOrEmpty(MyUser.password))
            {
                MyUser.password = BCrypt.Net.BCrypt.HashPassword(MyUser.password);
            }

            Console.WriteLine("wattttt2");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("wattttt5");

                return Page();
            }

            if (_svc.UpdateUser(MyUser) == true)
            {
                Console.WriteLine("wattttt3");
                TempData["AlertMessage"] = "You have Updated Your Account Successfully!";
                return RedirectToPage("/Account/Edit");
            }
            else
                return BadRequest();
        }
    }
}
