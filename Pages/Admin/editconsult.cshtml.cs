using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.Admin
{
    public class editconsultModel : PageModel
    {
        //public Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior AfterSaveBehavior { get; }
        //private readonly IWebHostEnvironment webHostEnvironment;
        private readonly Services.ConsultService _svc;
        public editconsultModel(Services.ConsultService service)
        {
            _svc = service;
            //webHostEnvironment = hostEnvironment;
        }
        //[BindProperty]
        //public ImageClass Image { get; set; }
        [BindProperty]
        public ConsultProfile Consult { get; set; }

        
        public IActionResult OnGet(int id)
        {
            //System.Diagnostics.Debug.WriteLine("here? : ");

            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                //System.Diagnostics.Debug.WriteLine("or here? : " + HttpContext.Session.GetInt32("user_id"));
                if (id != 0) //this is consultprofile id
                {
                    //System.Diagnostics.Debug.WriteLine("wb here? : ");
                    Consult = _svc.GetConsultById(id);
                    if (Consult != null)
                    {
                        //System.Diagnostics.Debug.WriteLine("ahere? : ");
                        return Page();
                    }
                    
                }
            }
            return NotFound();

            //if (HttpContext.Session.GetInt32("user_id") == null)
            //{
            //    System.Diagnostics.Debug.WriteLine("in here: "+ HttpContext.Session.GetInt32("user_id"));
            //    return NotFound();
            //}


            //if (id == 0)
            //{
            //    System.Diagnostics.Debug.WriteLine("or here: " + id);
            //    return NotFound();
            //}

            //Consult = _svc.GetConsultById(id);
            //if (Consult == null)
            //{
            //    return NotFound();
            //}
            //return Page();
        }



        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateConsult(Consult) == true)
            {

                return RedirectToPage("../Index");
            }
            else
                return BadRequest();
        }
    }
}