using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.amd
{ 
    public class amdwitnessModel : PageModel
    {
        [BindProperty]
        public AMDWitness amd { get; set; }
        private AMDService _svc;
        public amdwitnessModel(AMDService service)
        {
            _svc = service;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("error3");
            if (ModelState.IsValid)
            {
                Console.WriteLine("error1");
                if (_svc.AddAMD(amd))
                {
                    Console.WriteLine("error2");
                    // Create session

                    //HttpContext.Session.SetString("SSnric", amd.nric);
                    //HttpContext.Session.SetString("SSDept", MyEmployee.Department.ToString());
                    return RedirectToPage("amdview");
                }
                //else
                //{
                //    MyMessage = "Employee Id already exist!";
                //    return Page();
                //}
            }
            return Page();
        }
    }
}

