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
    public class EditFianceModel : PageModel
    {
        private readonly Services.WillService _svc;
        public EditFianceModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public MaritalInfo Fiance { get; set; }
        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Fiance = _svc.GetFianceById((int)HttpContext.Session.GetInt32("user_id"));
            if (Fiance == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            Fiance.OWNERID = (int)HttpContext.Session.GetInt32("user_id");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateFiance(Fiance) == true)
            {
                //redirects back to original page instead of willsummary
                return RedirectToPage("WillSummary");
            }
            else
                return BadRequest();
        }
    }
}
