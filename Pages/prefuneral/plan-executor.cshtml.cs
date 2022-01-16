using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class plan_executorModel : PageModel
    {
        private readonly Services.FExecutorPermissionService _svc;
        public plan_executorModel(Services.FExecutorPermissionService service)
        {
            _svc = service;
        }

        [BindProperty]
        public FExecutorPermission Permission { get; set; }

        // Sample Data
        class Executors
        {
            public int id { get; set; }
            public string fullname { get; set; }
            public string email { get; set; }

        }

        public IActionResult OnGet(string id)
        {
            // Validate if session exists
            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                // Testing Script
                HttpContext.Session.SetInt32("SSId", 2);
                //return NotFound();
            }

            int x = 0;
            Int32.TryParse(id, out x);
            List<FExecutorPermission> listAllPermission = _svc.GetAllPermissions();
            List<int> finalPermission = new List<int>(); ;
            foreach (var permission in listAllPermission)
            {
                if (permission.funeral_id == x)
                {
                    finalPermission.Add(permission.executor_id);
                    System.Diagnostics.Debug.WriteLine(permission.executor_id);
                }
            }
            
            return Page();
        }
    }
}
