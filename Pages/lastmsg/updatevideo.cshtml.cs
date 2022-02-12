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
    public class updatevideoModel : PageModel
    {
        private readonly Services.VideoMemoService _svc;
        public updatevideoModel(Services.VideoMemoService service)
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
            if (HttpContext.Session.GetInt32("IsUpdatingVideo") != 1)
            {
                return RedirectToPage("/index");
            }
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("VideoName")))
            {
                return LocalRedirect("/Video");
            }

            Video = _svc.GetVideoByWillMakerId(3); //temp var
            if (Video == null)
            {
                return NotFound();
            }

            Video.videoLink = HttpContext.Session.GetString("VideoName");
            if (_svc.UpdateVideo(Video) == true)
            {
                return RedirectToPage("/lastmsg/view");
            }
            else
                return BadRequest();

            return RedirectToPage("/lastmsg/view");
        }
    }
}
