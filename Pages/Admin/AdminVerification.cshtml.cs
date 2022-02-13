using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace afterlife_caretakers.Pages.Admin
{
    public class AdminVerificationModel : PageModel
    {
        //static void Main(string[] Args)
        //{
        //    SendEmail().Wait();
        //}
        private readonly UserService _svc;
        public AdminVerificationModel(UserService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Users MyUser { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyUser = _svc.GetUserByID(id);
            if (MyUser == null)
            {
                return NotFound();
            }
            Console.WriteLine(MyUser.email+"emaillltest");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateUser(MyUser) == true)
            {

                return RedirectToPage("./Admin/AdminVerificationList");
            }
            else
                return BadRequest();
        }

        

    }
}
