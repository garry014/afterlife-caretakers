using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.amd
{
    public class amdwitnessuModel : PageModel
    {
        private readonly Services.AMDService _svc;
        public amdwitnessuModel(Services.AMDService service)
        {
            _svc = service;
        }

        [BindProperty]
        public AMDWitness amd { get; set; }
        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            amd = _svc.GetWitnessById(id);
            if (amd == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateAMD(amd) == true)
            {

                return RedirectToPage("amdview");
            }
            else
                return BadRequest();
        }
    }
}
