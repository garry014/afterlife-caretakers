using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.doctor
{
    public class teamdoctorsModel : PageModel
    {
        [BindProperty]
        public List<ConsultProfile> allconsults { get; set; }

        [BindProperty]
        public ConsultProfile Consult { get; set; }

        private readonly ILogger<teamdoctorsModel> _logger;
        private ConsultService _svc;
        public teamdoctorsModel(ILogger<teamdoctorsModel> logger, ConsultService service)
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
