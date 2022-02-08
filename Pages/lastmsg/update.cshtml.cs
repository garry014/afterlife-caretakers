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
    public class updateModel : PageModel
    {
        private readonly Services.VideoMemoService _svc;
        public updateModel(Services.VideoMemoService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Video Video { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("SSId") == null)
            {
                // Testing Script
                HttpContext.Session.SetInt32("SSId", 3);
                //return NotFound();
            }
           

            Video = _svc.GetVideoByWillMakerId(3); //temp var
            if (Video == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_svc.UpdateVideo(Video) == true)
            {
                return RedirectToPage("/lastmsg/view");
            }
            else
                return BadRequest();
            
        }
    }
}
