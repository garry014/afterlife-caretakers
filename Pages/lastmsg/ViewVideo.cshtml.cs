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
    public class ViewVideoModel : PageModel
    {
        private readonly Services.BVideoPermissionService _svc;
        private readonly Services.VideoMemoService _vsvc;
        private readonly Services.UserService _usvc;

        public ViewVideoModel(Services.BVideoPermissionService service, Services.VideoMemoService vservice, Services.UserService uservice)
        {
            _svc = service;
            _vsvc = vservice;
            _usvc = uservice;
        }
        [BindProperty]
        public Video Video { get; set; }
        public Users User { get; set; }
        [BindProperty]
        public string ErrorMsg { get; set; }

        public IActionResult OnGet(string id)
        {
            
            if (HttpContext.Session.GetString("user_email") == null)
            {
                Console.WriteLine("1");
                return NotFound();
            }
            
            int x = 0;
            Int32.TryParse(id, out x);

            if (!_svc.PermissionMappingExists(HttpContext.Session.GetString("user_email"), x))
            {
                Console.WriteLine("2");
                return NotFound();
            }
            
            Video = _vsvc.GetVideoById(x);
            if (Video == null)
            {
                Console.WriteLine("3");
                return NotFound();
            }

            User = _usvc.GetUserByID(Video.willMakerID);
            if (User == null)
            {
                Console.WriteLine("4");
                return NotFound();
            }

            if (User.deathcert_upload == null)
            {
                ErrorMsg = "You can only view the video after the passing of your loved ones. If your loved ones has already passed on, please (get your executors to) upload a copy of the death cert.";
            }
            else if (User.deathdate_setting != null && User.deathdate_setting < DateTime.Now.AddDays(Video.releasePeriod * 7))
            {
                ErrorMsg = "Our deepest condolences. We are sorry, your loved ones had only enabled the viewing to you after " + DateTime.Now.AddDays(Video.releasePeriod * 7).ToString() + ". Please come back then.";
            }

            return Page();
        }
    }
}
