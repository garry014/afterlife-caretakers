using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.amd
{
    public class amdinfoModel : PageModel
    {
        private readonly Services.UserService _svc;
        [BindProperty]
        public Users MyUser { get; set; }

        public amdinfoModel(Services.UserService uservice)
        {
            _svc = uservice;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            var current_user = (int)HttpContext.Session.GetInt32("user_id");


            MyUser = _svc.GetUserByID(current_user);

            if (MyUser == null)
            {
                return NotFound();
            }


            return Page();
        }
    }
}
