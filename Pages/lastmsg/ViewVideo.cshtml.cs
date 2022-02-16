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
                return NotFound();
            }
            
            int x = 0;
            Int32.TryParse(id, out x);

            if (!_svc.PermissionMappingExists(HttpContext.Session.GetString("user_email"), x))
            {
                return NotFound();
            }
            
            Video = _vsvc.GetVideoById(x);
            if (Video == null)
            {
                return NotFound();
            }

            User = _usvc.GetUserByID(Video.willMakerID);
            if (User == null)
            {
                return NotFound();
            }

            DateTime myDate;
            if (DateTime.TryParse("1/1/2000", out myDate))
            {
                // empty
            }

            if (User.deathcert_upload == null)
            {
                ErrorMsg = "You can only view the video after the passing of your loved ones. If your loved ones has already passed on, please (get your executors to) upload a copy of the death cert.";
            }
            else if (User.deathdate_setting == myDate)
            {
                ErrorMsg = "Our deepest condolences. Our admins are still in the midst of processing the death certificate.";
            }
            else if (User.deathdate_setting.AddDays(Video.releasePeriod * 7) > DateTime.Now)
            {
                ErrorMsg = "Our deepest condolences. We are sorry, your loved ones has only enabled the viewing to you after " + User.deathdate_setting.AddDays(Video.releasePeriod * 7).ToString() + ". Please come back then.";
            }

            return Page();
        }
    }
}
