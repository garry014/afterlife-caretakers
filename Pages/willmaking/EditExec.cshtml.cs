using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;

namespace afterlife_caretakers.Pages.willmaking
{
    public class EditExecModel : PageModel
    {
        private readonly Services.WillService _svc;
        public EditExecModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public ExecutorInformation MyExecutor { get; set; }
        public IActionResult OnGet(int id)
        {
            MyExecutor = _svc.GetExecutorById(id);
            if (MyExecutor == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            MyExecutor.OWNERID = (int)HttpContext.Session.GetInt32("user_id");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateExecutor(MyExecutor) == true)
            {

                return RedirectToPage("willsummary");
            }
            else
                return BadRequest();
        }
    }
}
