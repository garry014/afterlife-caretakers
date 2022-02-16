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
        private readonly Services.UserService _usvc;
        private readonly Services.VideoMemoService _vsvc;
        public BViewModel(Services.BVideoPermissionService service, Services.UserService uservice, Services.VideoMemoService vservice)
        {
            _svc = service;
            _usvc = uservice;
            _vsvc = vservice;
        }

        [BindProperty]
        public List<BVideoPermission> AllPermissions { get; set; }
        [BindProperty]
        public string emailAddress { get; set; }
        public Users User { get; set; }
        public Video Video { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("user_email") == null)
            {
                return RedirectToPage("/Main_Login");
            }

            emailAddress = HttpContext.Session.GetString("user_email");
            AllPermissions = _svc.GetAllPermissions();

            
            return Page();
        }
    }
}
