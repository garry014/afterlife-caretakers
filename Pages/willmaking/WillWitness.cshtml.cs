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
    public class WillWitnessModel : PageModel
    {
        private readonly Services.WillService _svc;
        public WillWitnessModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public WitnessInformation MyWitness { get; set; }
        public List<WitnessInformation> ownerWitnessList;
        public IActionResult OnGet()
        {
            ownerWitnessList = _svc.GetWitnessFromOwner((int)HttpContext.Session.GetInt32("user_id"));
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
        public IActionResult OnPostWitnessBack()
        {
            return Page();

        }
        public IActionResult OnPostWitnessNext()
        {
            ownerWitnessList = _svc.GetWitnessFromOwner((int)HttpContext.Session.GetInt32("user_id"));
            return RedirectToPage("WillSummary");
        }
        public IActionResult OnPostAddWitness()
        {
            if (ModelState.IsValid)
            {
                MyWitness.OWNERID = (int)HttpContext.Session.GetInt32("user_id");

                if (_svc.AddWitness(MyWitness))
                {
                    ownerWitnessList = _svc.GetWitnessFromOwner((int)HttpContext.Session.GetInt32("user_id"));

                }
                else
                {
                    Console.WriteLine("Unable to add existing witness");
                    return Page();
                }
            }
            return Page();
        }
    }
}
