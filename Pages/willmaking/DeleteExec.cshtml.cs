using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;

namespace afterlife_caretakers.Pages.willmaking
{
    public class DeleteExecModel : PageModel
    {
        private readonly Services.WillService _svc;
        public DeleteExecModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public ExecutorInformation MyExecutor { get; set; }
        public List<ExecutorInformation> ownerExecList;
        public IActionResult OnGet(int id)
        {
            MyExecutor = _svc.GetExecutorById(id);
            if (MyExecutor == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Console.WriteLine("Im here");
            System.Diagnostics.Debug.WriteLine("test if id has been deleted");
            if (_svc.DeleteExecutor(MyExecutor))
            {
                return RedirectToPage("WillExecutor");
            }

            else
                return BadRequest();

            return Page();
        }
    }
}
