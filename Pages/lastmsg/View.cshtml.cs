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
        public ViewModel(Services.VideoMemoService service)
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

            Video = _svc.GetVideoByWillMakerId(3); //temp var
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
