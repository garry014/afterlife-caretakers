using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages
{
    public class SignatureRouteModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ESignatureFile")))
            {
                return NotFound();
            }

            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SignatureRedirectBack")))
            {
                return NotFound();
            }
            else
            {
                // DO NOT CHANGE THIS
                return Redirect(HttpContext.Session.GetString("SignatureRedirectBack"));
            }
            return NotFound();
        }
    }
}
