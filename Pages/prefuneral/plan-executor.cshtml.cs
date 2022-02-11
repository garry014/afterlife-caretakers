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
        [BindProperty]
        public FExecutorPermission Permission2 { get; set; }
        [BindProperty]
        public string Id { get; set; }

        // Sample Data
        public class Executors
        {
            public int id { get; set; }
            public string fullname { get; set; }
            public string email { get; set; }

        }
        [BindProperty]
        public List<Executors> Executor { get; set; }
        // End of sample data

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
            List<int> finalPermission = new List<int>();
            foreach (var permission in listAllPermission)
            {
                if (permission.funeral_id == x)
                {
                    finalPermission.Add(permission.executor_id);
                }
            }

            Id = id;
            return Page();
        }

        public IActionResult OnPost()
        {
            Permission.funeral_id = Int32.Parse(Id);
            if (_svc.DeletePermissions(Permission) && _svc.AddPermission(Permission))
            {
                System.Diagnostics.Debug.WriteLine(Permission2.executor_id);
                string referralLink = "/prefuneral/funeral-confirm?id=" + Id;
                HttpContext.Session.SetString("SignatureRedirectBack", referralLink);
                if (Permission2.executor_id != 0)
                {
                    if (_svc.AddPermission(Permission2))
                    {
                        return LocalRedirect("/Signature");
                    }
                    return Page();
                }
                return LocalRedirect("/Signature");
            }
            
            return Page();
        }
    }
}
