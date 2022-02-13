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
    public class ViewModel : PageModel
    {
        private readonly Services.VideoMemoService _svc;
        private readonly Services.BVideoPermissionService _bsvc;
        public ViewModel(Services.VideoMemoService service, Services.BVideoPermissionService bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }
        [BindProperty]
        public Video Video { get; set; }
        [BindProperty]
        public List<BVideoPermission> AllPermissions { get; set; }
        public IActionResult OnGet()
        {
            HttpContext.Session.SetInt32("user_id", 1);
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            Video = _svc.GetVideoByWillMakerId((int)HttpContext.Session.GetInt32("user_id"));
            Console.WriteLine("id: " + (int)HttpContext.Session.GetInt32("user_id"));
            AllPermissions = _bsvc.GetAllPermissions();
            if (Video == null)
            {
                return NotFound();
            }
            Console.WriteLine(Video.videoLink);
            return Page();
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetInt32("IsUpdatingVideo", 1);
            return LocalRedirect("/Video");
        }
    }
}
