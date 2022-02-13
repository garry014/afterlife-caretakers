using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace afterlife_caretakers.Pages.counsel
{
    public class teamcounsellorsModel : PageModel
    {
        [BindProperty]
        public List<ConsultProfile> allconsults { get; set; }

        [BindProperty]
        public ConsultProfile Consult { get; set; }

        private readonly ILogger<teamcounsellorsModel> _logger;
        private ConsultService _svc;
        public teamcounsellorsModel(ILogger<teamcounsellorsModel> logger, ConsultService service)
        {
            _logger = logger;
            _svc = service;
        }
        public void OnGet()
        {
            allconsults = _svc.GetAllConsults();

            //foreach (var i in allconsults)
            //{
            //    int x = 0;
            //    Int32.TryParse(id, out x);
            //    Consult = _svc.GetConsultById(x);
            //    System.Diagnostics.Debug.WriteLine("heere" + Consult.Id);
            //    if (Consult == null)
            //    {
            //        return NotFound();
            //    }
                
            //}
            //return Page();
        }
        public void OnPost()
        {

        }
    }
}
