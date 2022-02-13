using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.team
{
    public class editwitnessconsultModel : PageModel
    {
        //public Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior AfterSaveBehavior { get; }
        //private readonly IWebHostEnvironment webHostEnvironment;
        private readonly Services.WitnessService _wsvc;
        public editwitnessconsultModel(Services.WitnessService wservice)
        {
            _wsvc = wservice;
            //webHostEnvironment = hostEnvironment;
        }
        //[BindProperty]
        //public ImageClass Image { get; set; }
        //[BindProperty]
        //public ConsultProfile Consult { get; set; }
        [BindProperty]
        public WitnessConsult Witness { get; set; }

        public IActionResult OnGet(int id)
        {
            //System.Diagnostics.Debug.WriteLine("here? : ");

            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                //System.Diagnostics.Debug.WriteLine("or here? : " + HttpContext.Session.GetInt32("user_id"));
                if (id != 0) //this is consultprofile id
                {
                    //System.Diagnostics.Debug.WriteLine("wb here? : ");
                    Witness = _wsvc.GetWitConsultById(id);
                    if (Witness != null)
                    {
                        //System.Diagnostics.Debug.WriteLine("ahere? : ");
                        return Page();
                    }

                }
            }
            return NotFound();

        }



        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_wsvc.UpdateWitConsult(Witness) == true)
            {

                return RedirectToPage("../Index");
            }
            else
                return BadRequest();
        }
    }
}
