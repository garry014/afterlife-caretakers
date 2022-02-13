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
    public class EditWitnessModel : PageModel
    {
            private readonly Services.WillService _svc;
            public EditWitnessModel(Services.WillService service)
            {
                _svc = service;
            }
            [BindProperty]
            public WitnessInformation MyWitness { get; set; }
            public IActionResult OnGet(int id)
            {
            MyWitness = _svc.GetWitnessById(id);
                if (MyWitness == null)
                {
                    return NotFound();
                }
                return Page();
            }
            public IActionResult OnPost()
            {
            MyWitness.OWNERID = (int)HttpContext.Session.GetInt32("user_id");
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (_svc.UpdateWitness(MyWitness) == true)
                {

                    return RedirectToPage("WillSummary");
                }
                else
                    return BadRequest();
            }
        }
    }
