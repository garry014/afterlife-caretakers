using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;

namespace afterlife_caretakers.Pages.willmaking
{
    public class deleteGiftModel : PageModel
    {
        private readonly Services.WillService _svc;
        public deleteGiftModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Gift MyGift { get; set; }
        public List<Gift> ownerGiftList;
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
            if (_svc.DeleteSingleAsset(MyGift))
            {
                return RedirectToPage("specificgift");
            }
            //if (_svc.UpdateBeneficiary(MyBeneficiary) == true)
            //{
            //    //keeps redirecting here , but item is not deleted
            //    return RedirectToPage("WillForm3");
            //}
            else
                return BadRequest();

            return Page();
        }
    }
}