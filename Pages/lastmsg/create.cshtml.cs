using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.lastmsg
{
    public class createModel : PageModel
    {
        private readonly Services.VideoMemoService _svc;
        public createModel(Services.VideoMemoService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Video Video { get; set; }

        public IActionResult OnGet()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("VideoName"))){
                return LocalRedirect("/Video");
            }
            if (HttpContext.Session.GetInt32("IsUpdatingVideo") == 1)
            {
                return RedirectToPage("/lastmsg/updatevideo");
            }

            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                // Testing Script
                HttpContext.Session.SetInt32("SSId", 3);
                //return NotFound();
            }

            // if user alr have existing video, redirect them back
            Video = _svc.GetVideoByWillMakerId((int)HttpContext.Session.GetInt32("SSId")); //temp var
            if (Video != null)
            {
                return RedirectToPage("/lastmsg/view");
            }
            return Page();
        }

        public IActionResult OnPost(ImageClass Image)
        {
            if (ModelState.IsValid) // && session is valid
            {
                Video.videoLink = HttpContext.Session.GetString("VideoName"); 
                Video.willMakerID = (int)HttpContext.Session.GetInt32("SSId");
                if (_svc.AddVideo(Video))
                {
                    return RedirectToPage("/prefuneral/viewcasket");
                }
            }
            return Page();
        }

        
    }
}
