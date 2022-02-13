using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Http;

namespace afterlife_caretakers.Pages.willmaking
{
    public class WillFormModel : PageModel
    {
        [BindProperty]
        public PersonalInformation PersonalInfo { get; set; }

        private readonly Services.WillService _svc;
        private readonly Services.UserService _usvc;

        [BindProperty]
        public Users MyUser { get; set; }
        public WillFormModel(Services.WillService service, Services.UserService uservice)
        {
            PersonalInfo = new PersonalInformation();
            _svc = service;
            _usvc = uservice;
        }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "WillMaker")
            {
                return Page();
            }
            MyUser = _usvc.GetUserByID((int)HttpContext.Session.GetInt32("user_id"));
            if (WillService.PersonalInfo != null)
            {
                PersonalInfo = WillService.PersonalInfo;
            }
            Console.WriteLine("On Get Will Form 1");
            return NotFound();
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
