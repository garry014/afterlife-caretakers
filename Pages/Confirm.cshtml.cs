using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pract2.Pages
{
    public class ConfirmModel : PageModel
    {
        [BindProperty]
        public String PageMessage { get; set; }
        public IActionResult OnGet()
        {
            if(!String.IsNullOrEmpty(HttpContext.Session.GetString("SSName")))
         
            { 
                PageMessage = "Welcome "+ HttpContext.Session.GetString("SSName") + " To " + HttpContext.Session.GetString("SSDept");

                HttpContext.Session.Clear();
                return Page();
            }
            
             return Redirect("Create");
          

        }
    }
}