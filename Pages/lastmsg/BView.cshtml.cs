using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.lastmsg
{
    public class BViewModel : PageModel
    {
        private readonly Services.BVideoPermissionService _svc;
        public BViewModel(Services.BVideoPermissionService service)
        {
            _svc = service;
        }

        [BindProperty]
        public List<BVideoPermission> Permissions { get; set; }
        //public IActionResult OnGet()
        //{
        //    if (HttpContext.Session.GetInt32("user_id") == null)
        //    {
        //        return NotFound();
        //    }

        //    int x = 0;
        //    Int32.TryParse(id, out x);
        //    List<FExecutorPermission> listAllPermission = _svc.GetAllPermissions();
        //    List<int> finalPermission = new List<int>();
        //    foreach (var permission in listAllPermission)
        //    {
        //        if (permission.funeral_id == x)
        //        {
        //            finalPermission.Add(permission.executor_id);
        //        }
        //    }
        //    Permissions = _svc.GetPermissionByBId(HttpContext.Session.GetString("user_email"));
        //}
    }
}
