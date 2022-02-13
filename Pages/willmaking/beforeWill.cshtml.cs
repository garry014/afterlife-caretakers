using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.willmaking
{
    public class beforeWillModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnStaceyPost()
        {
            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("WillManual");
        }
    }
}
