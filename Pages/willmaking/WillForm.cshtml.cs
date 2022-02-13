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
        //[BindProperty]
        //public PersonalInformation PersonalInfo { get; set; }

        private readonly Services.WillService _svc;
        private readonly Services.UserService _usvc;

        [BindProperty]
        public Users MyUser { get; set; }
        public WillFormModel(Services.WillService service, Services.UserService uservice)
        {
            //PersonalInfo = new PersonalInformation();
            _svc = service;
            _usvc = uservice;
        }
        public IActionResult OnGet(int id)
        {

            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "WillMaker")
            {
                var current_user = (int)HttpContext.Session.GetInt32("user_id");
                Console.WriteLine("Edit with " + current_user);


                MyUser = _usvc.GetUserByID(current_user);

                if (MyUser == null)
                {
                    Console.WriteLine("id found:" + MyUser.name);
                    return NotFound();
                }
                return Page();
            }
            return NotFound();
        }

        public IActionResult OnPostFirstBack()
        {
            return Redirect("WillManual");
        }

        
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {

                return Page();
            }

            if (_usvc.UpdateUser(MyUser) == true)
            {
                return RedirectToPage("WillForm2");
            }
            else
                return BadRequest();
            // jump to 2nd part
        }

        public IActionResult ClickNext()
        {
            if (ModelState.IsValid)
            {
                if (_usvc.UpdateUser(MyUser) == true)
                {
                    return RedirectToPage("/WillForm2");
                }
            }
            return Page();
        }
    }
}
