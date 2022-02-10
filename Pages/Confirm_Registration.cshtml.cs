using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages
{
    public class Confirm_RegistrationModel : PageModel
    {
        [BindProperty]
        public String PageMessage { get; set; }
        public IActionResult OnGet()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("name")))

            {
                PageMessage = "Welcome " + HttpContext.Session.GetString("name") + " To " + HttpContext.Session.GetString("email");

                HttpContext.Session.Clear();
                return Page();
            }

            return Redirect("Confirm_Registration");

        }
        
        
    }
}
