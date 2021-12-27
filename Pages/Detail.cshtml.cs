using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pract2.Models;
using Pract2.Services;

namespace Pract2.Pages
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public Employee MyEmployee { get; set; }


        private readonly EmployeeService _svc;
        public DetailModel(EmployeeService service )
        {
            _svc = service;
        }
      
        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                MyEmployee = _svc.GetEmployeeById(id);
                return Page();
            }
            else
                return RedirectToPage("Index");
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}