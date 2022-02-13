using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;

namespace afterlife_caretakers.Pages.willmaking
{
    public class DeleteWitnessModel : PageModel
    {
        private readonly Services.WillService _svc;
        public DeleteWitnessModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public WitnessInformation MyWitness { get; set; }
        public List<WitnessInformation> ownerWitnessList;
        public IActionResult OnGet(int id)
        {
            MyWitness = _svc.GetWitnessById(id);
            if (MyWitness == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Console.WriteLine("Im here");
            System.Diagnostics.Debug.WriteLine("test if id has been deleted");
            if (_svc.DeleteWitness(MyWitness))
            {
                return RedirectToPage("WillWitness");
            }

            else
                return BadRequest();

            return Page();
        }
    }
}
