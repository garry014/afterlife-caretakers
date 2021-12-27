using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pract2.Models;

namespace Pract2.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Services.EmployeeService _svc;
        public CreateModel(Services.EmployeeService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Employee MyEmployee { get; set; }
        [BindProperty]
        public string MyMessage {get; set;}
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddEmployee(MyEmployee))
                {
                    // Create session

                    HttpContext.Session.SetString("SSName", MyEmployee.Name);
                    HttpContext.Session.SetString("SSDept", MyEmployee.Department.ToString());
                    return RedirectToPage("Confirm");
                }
                else
                {
                    MyMessage = "Employee Id already exist!";
                    return Page();
                }
            }
            return Page();
        }
    }
}