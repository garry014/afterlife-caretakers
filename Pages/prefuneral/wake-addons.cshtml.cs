using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class wake_addonsModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public wake_addonsModel(Services.FuneralService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Funeral Funeral { get; set; }
        [BindProperty]
        public FuneralPricing fp { get; set; }

        public IActionResult OnGet(string id)
        {
            // Validate if session exists
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            int x = 0;
            Int32.TryParse(id, out x);
            Funeral = _svc.GetFuneralByFuneralId(x);

            if (Funeral == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(Funeral.Religion))
            {
                return Redirect("/prefuneral/religion?id=" + Funeral.Id);
            }
            if (Funeral.WakePostalCode == "999999")
            {
                return Redirect("/prefuneral/wake-location?id=" + Funeral.Id);
            }
            if (Funeral.CasketID == 0)
            {
                return Redirect("/prefuneral/wake-plans?id=" + Funeral.Id);
            }

            fp = new FuneralPricing();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }
            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("user_id");

            var included = new[] { "HasMakeupServices", "HasHairstylingServices", "HasMobileToilet", "HasBeverages", "HasTibits", "HasFridge", "HasLunch", "HasDinner", "HasRegisterBook", "HasMemorialFolders" };
            if (_svc.UpdateFuneral(Funeral, included) == true)
            {
                return Redirect("/prefuneral/funeral-plans?id=" + Funeral.Id);
            }
            else
                return BadRequest();
        }
    }
}
