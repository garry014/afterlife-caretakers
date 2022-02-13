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
            
            MyBeneficiary = _svc.GetBeneficiaryFromOwner((int)HttpContext.Session.GetInt32("user_id"));
            ownerGiftList = _svc.GetGiftFromOwner((int)HttpContext.Session.GetInt32("user_id"));
            ownerExecList = _svc.GetExecutorFromOwner((int)HttpContext.Session.GetInt32("user_id"));
            ownerWitnessList = _svc.GetWitnessFromOwner((int)HttpContext.Session.GetInt32("user_id"));
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

