using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace afterlife_caretakers.Pages.willmaking
{
    public class specificgiftModel : PageModel
    {
        public List<Gift> ownerGiftList;
        public List<SelectListItem> BeneficiarySelections = new List<SelectListItem>();
        public Dictionary<int, BeneficiaryInformation> beneDictionary = new Dictionary<int, BeneficiaryInformation>();
        SelectListItem beneSelection = new SelectListItem();
        private WillService _svc;
        public specificgiftModel(WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public List<BeneficiaryInformation> MyBeneficiary { get; set; }
        [BindProperty]
        public Gift MyGift { get; set; }

        public void initData()
        {

            //neeed to add this to display the contents of table
            ownerGiftList = _svc.GetGiftFromOwner((int)HttpContext.Session.GetInt32("user_id"));
            //displaying beneficiary into form
            MyBeneficiary = _svc.GetBeneficiaryFromOwner((int)HttpContext.Session.GetInt32("user_id"));
   
            foreach (BeneficiaryInformation b in MyBeneficiary)
            {
                SelectListItem s = new SelectListItem(b.NAME, Convert.ToString(b.Id));
                BeneficiarySelections.Add(s);
                beneDictionary[b.Id] = b;
            }
        }
        public IActionResult OnGet(int id)
        {
            
            initData();
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
        public IActionResult OnPostGiftBack()
        {
            return Redirect("ChoiceOfGift");

        }
        public IActionResult OnPostGiftNext()
        {
                //return Page();
                return RedirectToPage("WillExecutor");

        }
        public IActionResult OnPostGiftAdd()
        {
            if (ModelState.IsValid)
            {
                MyGift.OWNERID = (int)HttpContext.Session.GetInt32("user_id");

                // trying to add bene to owner into db
                if (_svc.AddAsset(MyGift))
                {
                    Console.WriteLine("Add new asset");
                    // grab from DB again all the gift of this owner
                    //ownerGiftList = _svc.GetGiftFromOwner((int)HttpContext.Session.GetInt32("user_id"));
                    initData();
                }
                else
                {
                    Console.WriteLine("Unable to add existing gift");
                    return Page();
                }
            }
            //initData();
            return Page();
            //works with Page() when calling initdata function again 
            //same 
           // return RedirectToPage("specificgift");
        }
    }
}
