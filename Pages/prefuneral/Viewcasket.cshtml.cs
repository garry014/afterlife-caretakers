using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class ViewcasketModel : PageModel
    {
        [BindProperty]
        public List<Casket> allcaskets { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private CasketService _svc;
        public ViewcasketModel(ILogger<IndexModel> logger, CasketService service)
        {
            _logger = logger;
            _svc = service;
        }
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "Admin")
            {
                allcaskets = _svc.GetAllCaskets();
                return Page();
            }
            return NotFound();
        }
        public void OnPost()
        {

        }
    }
}
