using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.willmaking
{
    public class WillManualModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "WillMaker")
            {
                return Page();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("WillForm");
        }
    }
}
