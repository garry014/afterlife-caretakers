using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;

namespace afterlife_caretakers.Pages.willmaking
{
    public class deleteBeneModel : PageModel
    {
        private readonly Services.WillService _svc;
        public deleteBeneModel(Services.WillService service)
        {
            _svc = service;
        }
        //beneficiary information
        [BindProperty]
        public BeneficiaryInformation MyBeneficiary { get; set; }
        public Gift MyGift { get; set; }
        public List<BeneficiaryInformation> ownerBeneList;
        public IActionResult OnGet(int id)
        {
            MyBeneficiary = _svc.GetBeneficiaryId(id);
            //System.Diagnostics.Debug.WriteLine("id of asset",MyGift.BeneID);
            //MyGift = _svc.GetAssetById(id);
            //System.Diagnostics.Debug.WriteLine("id of asset", MyGift.BeneID);
            // tried with && MyGift null
            if (MyBeneficiary == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            System.Diagnostics.Debug.WriteLine("test if id has been deleted");
            //MyGift.Id = MyBeneficiary.Id;
            if (_svc.DeleteBeneficiary(MyBeneficiary))
            {
                if (_svc.DeleteAsset(id))
                {
                    return RedirectToPage("WillForm3");
                }
                //returned to summary page just to check if code comes here it doesnt even go here , only checks deletebene.cs
                return BadRequest();
            }
            else
                return BadRequest();

            return Page();
        }
    }
}
