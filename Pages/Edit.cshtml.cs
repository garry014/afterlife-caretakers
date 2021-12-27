using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pract2.Models;

namespace Pract2
{
    public class EditModel : PageModel
    {
        private readonly Services.EmployeeService _svc;
        public EditModel(Services.EmployeeService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Employee MyEmployee { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyEmployee = _svc.GetEmployeeById(id);
            if (MyEmployee == null)
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

            if (_svc.UpdateEmployee(MyEmployee) == true)
            {

                return RedirectToPage("./Index");
            }
            else
                return BadRequest();
        }

    }
}
