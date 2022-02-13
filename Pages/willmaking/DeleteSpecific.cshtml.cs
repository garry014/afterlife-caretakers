using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;

namespace afterlife_caretakers.Pages.willmaking
{
    public class DeleteSpecificModel : PageModel
    {
        private readonly Services.WillService _svc;
        public DeleteSpecificModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Gift MyGift { get; set; }
        public List<Gift> ownergiftList;
        public IActionResult OnGet(int id)
        {
            MyGift = _svc.GetAssetById(id);
            if (MyGift == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Console.WriteLine("Im here");
            System.Diagnostics.Debug.WriteLine("test if id has been deleted");
            if (_svc.DeleteSingleAsset(MyGift))
            {
                return RedirectToPage("specificgift");
            }
            else
                return BadRequest();

            return Page();
        }
    }
}
