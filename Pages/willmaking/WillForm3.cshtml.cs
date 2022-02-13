using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace afterlife_caretakers.Pages.willmaking
{
    public class WillForm3Model : PageModel
    {

        public List<BeneficiaryInformation> ownerBeneList;
        public Gift MyGift { get; set; }

        private readonly Services.WillService _svc;
        [BindProperty]
        public BeneficiaryInformation MyBeneficiary { get; set; }

        public WillForm3Model(Services.WillService service)
        {
            _svc = service;
        }
        //getting bene off owner id
        public void OnGet()
        {
            ownerBeneList = _svc.GetBeneficiaryFromOwner(88);
        }
        public IActionResult OnPostForm3Back()
        {
            return Redirect("WillForm2");
            
        }
        public IActionResult OnPostForm3Next()
        {
            ownerBeneList = _svc.GetBeneficiaryFromOwner(88);
            return RedirectToPage("ChoiceOfGift");
            //if (ModelState.IsValid)
            //{
            //    //return Page();
            //    return RedirectToPage("ChoiceOfGift");
            //}
            return Page();
        }
        public IActionResult OnPostForm3AddBene()
        {
            if (ModelState.IsValid)
            {
                // Add Beneficiary if exist
                //BeneficiaryInformation newBene = new BeneficiaryInformation(MyBeneficiary);
                //MyBeneficiary.Id = 100;
                MyBeneficiary.OWNERID = 88;

                // trying to add bene to owner into db
                if (_svc.AddBeneficiary(MyBeneficiary))
                {
                    Console.WriteLine("Add new bene");
                    // grab from DB again all the bene of this owner
                    ownerBeneList = _svc.GetBeneficiaryFromOwner(88);
                    
                }
                else
                {
                    Console.WriteLine("Unable to add existing bene");
                    //MyMessage = "Employee Id already exist!";
                    return Page();
                }
            }
            return RedirectToPage("WillForm3");
            //return RedirectToPage("ChoiceOfGift");
        }

    }
}
