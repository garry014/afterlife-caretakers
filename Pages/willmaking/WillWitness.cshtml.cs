using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;

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
        public void OnGet()
        {
            ownerWitnessList = _svc.GetWitnessFromOwner(88);
        }
        public IActionResult OnPostWitnessBack()
        {
            return Page();

        }
        public IActionResult OnPostWitnessNext()
        {
            ownerWitnessList = _svc.GetWitnessFromOwner(88);
            return RedirectToPage("WillSummary");
        }
        public IActionResult OnPostAddWitness()
        {
            if (ModelState.IsValid)
            {
                MyWitness.OWNERID = 88;

                if (_svc.AddWitness(MyWitness))
                {
                    ownerWitnessList = _svc.GetWitnessFromOwner(88);

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
