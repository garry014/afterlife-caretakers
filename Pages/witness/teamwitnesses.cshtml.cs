using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.witness
{
    public class teamwitnessesModel : PageModel
    {
        [BindProperty]
        public List<WitnessConsult> allconsults { get; set; }

        //[BindProperty]
        //public ConsultProfile Consult { get; set; }
        [BindProperty]
        public WitnessConsult Witness { get; set; }

        private readonly ILogger<teamwitnessesModel> _logger;
        private WitnessService _wsvc;
        public teamwitnessesModel(ILogger<teamwitnessesModel> logger, WitnessService wservice)
        {
            _logger = logger;
            _wsvc = wservice;
        }
        public void OnGet()
        {
            
            allconsults = _wsvc.GetAllWitConsults();
            System.Diagnostics.Debug.WriteLine("pppppppppp" + allconsults);
        }
        public void OnPost()
        {

        }
    }
}
