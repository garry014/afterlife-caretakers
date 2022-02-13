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
    public class addbeneModel : PageModel
    {
        private readonly Services.VideoMemoService _svc;
        private readonly Services.BVideoPermissionService _bsvc;
        public addbeneModel(Services.VideoMemoService service, Services.BVideoPermissionService bservice)
        {
            _svc = service;
            _bsvc = bservice;
        }
        [BindProperty]
        public Video Video { get; set; }
        [BindProperty]
        public BVideoPermission Permission { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            Video = _svc.GetVideoByWillMakerId((int)HttpContext.Session.GetInt32("user_id"));
            
            if (Video == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_bsvc.AddVideo(Permission))
                {
                    return RedirectToPage("/lastmsg/view");
                }
                else
                    return NotFound();
            }
            return Page();
        }
    }
}
