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

            // 3. Add a else if loop, to redirect back to your route
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SignatureReferral")))
            {
                return NotFound();
            }
            else if (HttpContext.Session.GetString("SignatureReferral") == "funeral") // example
            {
                return RedirectToPage("/prefuneral/funeral-confirm");
            }
            return NotFound();
        }
    }
}
