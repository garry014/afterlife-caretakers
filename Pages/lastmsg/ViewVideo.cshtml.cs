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

        public ViewVideoModel(Services.BVideoPermissionService service, Services.VideoMemoService vservice)
        {
            _svc = service;
            _vsvc = vservice;
        }
        [BindProperty]
        public Video Video { get; set; }

        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return NotFound();
            }
            Console.WriteLine("1");
            int x = 0;
            Int32.TryParse(id, out x);
            
            if (!_svc.PermissionMappingExists(HttpContext.Session.GetString("email"), x))
            {
                return NotFound();
            }
            Console.WriteLine("2");
            Video = _vsvc.GetVideoById(x);
            if (Video == null)
            {
                return NotFound();
            }
            Console.WriteLine("3");
            return Page();
        }
    }
}
