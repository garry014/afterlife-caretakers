using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using afterlife_caretakers.Services;

namespace afterlife_caretakers.Pages.willmaking
{
    public class WillForm2Model : PageModel
    {

        private readonly Services.WillService _svc;
        private readonly UserService _usvc;

        public WillForm2Model(Services.WillService service,Services.UserService uservice)
        {
            _svc = service;
            _usvc = uservice;
            //MaritalInfo = new MaritalInfomation();
        }
        //[BindProperty]
        //public Users MyUser { get; set; }
        [BindProperty]
        public MaritalInfo MyMarital { get; set; }
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

        public IActionResult OnPostFianceBack()
        {
            return Redirect("WillForm");
        }
        public IActionResult OnPostFianceNext()
        {
            MyMarital.Mstatus = "Married";
            return Redirect("WillForm3");
        }
        public IActionResult OnPostAddFiance()
        {
            MyMarital.Mstatus = "Married";
            if (ModelState.IsValid)
            {
                MyMarital.Mstatus = "Married";
                MyMarital.OWNERID = (int)HttpContext.Session.GetInt32("user_id");

                if (_svc.AddFiance(MyMarital))
                {
                    return RedirectToPage("WillForm3");
                }
                else
                {
                    //MyMessage = "Employee Id already exist!";
                    return Page();
                }
            }
            return Page();
        }

    }
}
