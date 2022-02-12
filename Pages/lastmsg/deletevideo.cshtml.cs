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
    public class deletevideoModel : PageModel
    {
        private readonly Services.VideoMemoService _svc;
        public deletevideoModel(Services.VideoMemoService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Video Video { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }
            Video = _svc.GetVideoByWillMakerId((int)HttpContext.Session.GetInt32("user_id")); //temp var
            if (Video == null)
            {
                return NotFound();
            }

            if (_svc.DeleteVideo(Video) == true)
            {
                return RedirectToPage("/prefuneral/viewcasket");
            }
            else
                return BadRequest();
        }
    }
}
