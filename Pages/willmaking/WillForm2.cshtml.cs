using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;

namespace afterlife_caretakers.Pages.willmaking
{
    public class WillForm2Model : PageModel
    {

        private readonly Services.WillService _svc;
        public WillForm2Model(Services.WillService service)
        {
            _svc = service;
            //MaritalInfo = new MaritalInfomation();
        }
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
            return Redirect("WillForm3");
        }
        public IActionResult OnPostAddFiance()
        {
            if (ModelState.IsValid)
            {

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
