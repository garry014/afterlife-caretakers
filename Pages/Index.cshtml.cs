using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Pract2.Models;
using Pract2.Services;

namespace Pract2.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Employee> allemployees { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private EmployeeService _svc;
        public IndexModel(ILogger<IndexModel> logger ,EmployeeService service )
        {
            _logger = logger;
            _svc = service;
        }

        public void OnGet()
        {
            allemployees = _svc.GetAllEmployees();
        }
        public void OnPost()
        {  
            
         }

    }
}
