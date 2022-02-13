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
    public class doctorprofileModel : PageModel
    {
        [BindProperty]
        public ConsultProfile Consult { get; set; }

        [BindProperty]
        public List<ConsultProfile> allconsults { get; set; }

        private readonly ConsultService _svc;
        private readonly ILogger<teamdoctorsModel> _logger;
        public doctorprofileModel(ILogger<teamdoctorsModel> logger, ConsultService service)
        {
            _logger = logger;
            _svc = service;
        }
        public IActionResult OnGet(int id)
        {
            allconsults = _svc.GetAllConsults();

            if (id != 0) //thereisid
            {
                Consult = _svc.GetConsultById(id);

                return Page();
            }
            else
            {
                return RedirectToPage("/doctor/teamdoctors");
            }
        }
    }
}
