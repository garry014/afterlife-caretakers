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
    public class WillSummaryModel : PageModel
    {
        private readonly Services.WillService _svc;
        public WillSummaryModel(Services.WillService service)
        {
            _svc = service;

        }
        [BindProperty]
        public  List<BeneficiaryInformation> MyBeneficiary { get; set; }
        [BindProperty]
        public PersonalInformation PersonalInfo { get; set; }
        [BindProperty]
        public MaritalInfo MyMarital { get; set; }
        [BindProperty]
        public ExecutorInformation MyExecutor { get; set; }
        [BindProperty]
        public WitnessInformation MyWitness { get; set; }
        [BindProperty]
        public List<Gift> ownerGiftList { get; set; }
        [BindProperty]
        public List<ExecutorInformation> ownerExecList { get; set; }
        [BindProperty]
        public List<WitnessInformation> ownerWitnessList { get; set; }
        public Dictionary<int, BeneficiaryInformation> beneDictionary = new Dictionary<int, BeneficiaryInformation>();
        public List<SelectListItem> BeneficiarySelections = new List<SelectListItem>();
        SelectListItem beneSelection = new SelectListItem();
        public IActionResult OnGet(int id)
        {
            
            MyBeneficiary = _svc.GetBeneficiaryFromOwner(88);
            ownerGiftList = _svc.GetGiftFromOwner(88);
            ownerExecList = _svc.GetExecutorFromOwner(88);
            ownerWitnessList = _svc.GetWitnessFromOwner(88);
            initData();
            ////owner id == willmaker id
            if (MyBeneficiary == null)
            {
                return NotFound();
            }
            if (ownerGiftList == null)
            {
                return NotFound();
            }
            if (ownerExecList == null)
            {
                return NotFound();
            }
            if (ownerWitnessList == null)
            {
                return NotFound();
            }
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

        public IActionResult OnPostSummaryNext()
        {
            //return Page();
            return RedirectToPage("Payment");

        }
        public IActionResult OnPostExecBack()
        {
            //return Page();
            return RedirectToPage("willmaking/EditExec");

        }

    }

    }

