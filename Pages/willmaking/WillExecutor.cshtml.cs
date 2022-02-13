using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace afterlife_caretakers.Pages.willmaking
{
    public class WillExecutorModel : PageModel
    {
        private readonly Services.WillService _svc;
        public WillExecutorModel(Services.WillService service)
        {
            _svc = service;
        }
        [BindProperty]
        public ExecutorInformation MyExecutor { get; set; }
        public List<ExecutorInformation> ownerExecList;
        //Executor at least 1 sub executor thus can have more than 1.
        public List<Gift> ownerGiftList;
        public List<SelectListItem> BeneficiarySelections = new List<SelectListItem>();
        public Dictionary<int, BeneficiaryInformation> beneDictionary = new Dictionary<int, BeneficiaryInformation>();
        SelectListItem beneSelection = new SelectListItem();
        public Gift MyGift { get; set; }
        public List <BeneficiaryInformation> MyBeneficiary { get; set; }
        public IActionResult OnGet()
        {
            ownerExecList = _svc.GetAllExecutor(88);
            initData();
            return Page();
        }
        public void initData()
        {

            //neeed to add this to display the contents of table
            ownerGiftList = _svc.GetGiftFromOwner(88);
            //displaying beneficiary into form
            MyBeneficiary = _svc.GetBeneficiaryFromOwner(88);

            foreach (BeneficiaryInformation b in MyBeneficiary)
            {
                SelectListItem s = new SelectListItem(b.NAME, Convert.ToString(b.Id));
                BeneficiarySelections.Add(s);
                beneDictionary[b.Id] = b;
            }
        }
        public IActionResult OnPostExecBack()
        {
            return Page();

        }
        public IActionResult OnPostExecNext()
        {
            ownerExecList = _svc.GetAllExecutor(88);
            initData();
            return RedirectToPage("WillWitness");
        }
        public IActionResult OnPostAddExec()
        {
            //add init data onload new page so that u can see the beneficiary selected.
            initData();
            if (ModelState.IsValid)
            {
                MyExecutor.OWNERID = 88;

                // trying to add exec to owner into db
                if (_svc.AddExecutor(MyExecutor))
                {
                    Console.WriteLine("Add new exec");
                    // grab from DB again all the exec of this owner
                    ownerExecList = _svc.GetExecutorFromOwner(88);

                }
                else
                {
                    Console.WriteLine("Unable to add existing executor");
                    return Page();
                }
            }
            return Page();
        }

    }
}

