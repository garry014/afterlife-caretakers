using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace afterlife_caretakers.Pages.willmaking
{
    public class EditGiftModel : PageModel
    {
        public List<Gift> ownerGiftList;
        public List<SelectListItem> BeneficiarySelections = new List<SelectListItem>();
        public Dictionary<int, BeneficiaryInformation> beneDictionary = new Dictionary<int, BeneficiaryInformation>();
        SelectListItem beneSelection = new SelectListItem();
        public List<BeneficiaryInformation> ownerbeneList { get; set; }
        public BeneficiaryInformation MyBeneficiary { get; set; }
        //maybe need to add the selectlist for bene
        private readonly Services.WillService _svc;
        public EditGiftModel(Services.WillService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Gift MyGift { get; set; }
        public IActionResult OnGet(int id)
        {
            //MyBeneficiary = _svc.GetBeneficiaryId(id);
            //initData();
            MyGift = _svc.GetAssetById(id);
            if (MyGift == null)
            {
                return NotFound();
            }
            return Page();
        }
        //public void initData()
        //{

        //    //neeed to add this to display the contents of table
        //    ownerGiftList = _svc.GetGiftFromOwner(88);
        //    //displaying beneficiary into form
        //    ownerbeneList = _svc.GetBeneficiaryFromOwner((int)HttpContext.Session.GetInt32("user_id"));

        //    foreach (BeneficiaryInformation b in ownerbeneList)
        //    {
        //        SelectListItem s = new SelectListItem(b.NAME, Convert.ToString(b.Id));
        //        BeneficiarySelections.Add(s);
        //        beneDictionary[b.Id] = b;
        //    }
        //}
        public IActionResult OnPost()
        {
            //MyBeneficiary.OWNERID = (int)HttpContext.Session.GetInt32("user_id");
            //ownerbeneList = _svc.GetBeneficiaryFromOwner((int)HttpContext.Session.GetInt32("user_id"));
            //initData();
            MyGift.OWNERID = (int)HttpContext.Session.GetInt32("user_id");


            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateAsset(MyGift) == true)
            {
                //redirects back to original page instead of willsummary
                return RedirectToPage("specificgift");
            }
            else
                return BadRequest();
        }
    }
}
