using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.advisor
{
    public class teamadvisorsModel : PageModel
    {
        [BindProperty]
        public List<ConsultProfile> allconsults { get; set; }

        [BindProperty]
        public ConsultProfile Consult { get; set; }

        private readonly ILogger<teamadvisorsModel> _logger;
        private ConsultService _svc;
        public teamadvisorsModel(ILogger<teamadvisorsModel> logger, ConsultService service)
        {
            _logger = logger;
            _svc = service;
        }
        public void OnGet()
        {
            allconsults = _svc.GetAllConsults();
        }
        public void OnPost()
        {

        }
    }
}
