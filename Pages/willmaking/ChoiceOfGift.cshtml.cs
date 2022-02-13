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
    public class ChoiceOfGiftModel : PageModel
    {
        private readonly Services.WillService _svc;
        public ChoiceOfGiftModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Gift MyGift { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("usertype") == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("usertype") == "WillMaker")
            {
                return Page();
            }
            return Page();
        }
        public IActionResult OnPostChoiceBack()
        {
            return Redirect("WillForm3");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddAsset(MyGift))
                {
                    MyGift.description = "";
                    System.Diagnostics.Debug.WriteLine(MyGift.TYPE);
                    if (MyGift.TYPE == "Specific")
                    {
                        
                        return RedirectToPage("specificgift");
                    }
                    else
                    {
                        return RedirectToPage("DistributionofAssets");
                    }
                }
                else
                {
                    return Page();
                }
            }

            return Page();

        }
        
    }
}
