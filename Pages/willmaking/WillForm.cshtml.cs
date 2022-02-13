using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;

namespace afterlife_caretakers.Pages.willmaking
{
    public class WillFormModel : PageModel
    {
        [BindProperty]
        public PersonalInformation PersonalInfo { get; set; }

        public WillFormModel()
        {
            PersonalInfo = new PersonalInformation();
        }
        public void OnGet()
        {
            if (WillService.PersonalInfo != null)
            {
                PersonalInfo = WillService.PersonalInfo;
            }
            Console.WriteLine("On Get Will Form 1");
        }

        public IActionResult OnPostFirstBack()
        {
            //WillService.PersonalInfo = null;
            return Redirect("WillManual");
        }

        
        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            WillService.PersonalInfo = PersonalInfo;
            // jump to 2nd part
            return RedirectToPage("WillForm2");
        }

        public void ClickNext()
        {
            if (ModelState.IsValid)
            {
                WillService.PersonalInfo = PersonalInfo;

                // jump to 2nd part
            }
        }
    }
}
