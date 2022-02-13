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
    public class EditBeneModel : PageModel
    {
        private readonly Services.WillService _svc;
        public EditBeneModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public BeneficiaryInformation MyBeneficiary { get; set; }
        public IActionResult OnGet(int id)
        {
            MyBeneficiary = _svc.GetBeneficiaryId(id);
            if (MyBeneficiary == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            MyBeneficiary.OWNERID = (int)HttpContext.Session.GetInt32("user_id");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateBeneficiary(MyBeneficiary) == true)
            {
                //instead of redirect to willsummary, redirect back to the form to see changes immediately.
                return RedirectToPage("WillForm3");
            }
            else
                return BadRequest();
        }
    }
}
