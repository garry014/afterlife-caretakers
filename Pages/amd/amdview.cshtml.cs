using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.amd
{
    public class amdviewModel : PageModel
    {
        [BindProperty]
        public List<AMDWitness> AllWitness { get; set; }
        private readonly ILogger<amdviewModel> _logger;
        public AMDWitness MyWitness { get; set; }


        private readonly AMDService _svc;
        public amdviewModel(ILogger<amdviewModel> logger, AMDService service)
        {
            _logger = logger;
            _svc = service;
        }

        public void OnGet()
        {
            AllWitness = _svc.GetAllWitness();
          
        }

        public void OnPost()
        {
            
        }
    }
}
