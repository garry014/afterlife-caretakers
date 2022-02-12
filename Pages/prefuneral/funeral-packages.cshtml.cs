using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class funeral_packagesModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public funeral_packagesModel(Services.FuneralService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Funeral Funeral { get; set; }
        [BindProperty]
        public int PackageId { get; set; }
        [BindProperty]
        public string errorMsg { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine(PackageId);
            if (HttpContext.Session.GetInt32("user_id") != null)
            {
                Funeral funeral = _svc.GetFuneralByUserId((int)HttpContext.Session.GetInt32("user_id"));
                if (funeral != null)
                {
                    errorMsg = "You have an existing plan, please delete your current plan before enrolling into a package.";
                    return Page();
                }
                else
                {
                    if (PackageId == 1)
                    {
                        generatePackage("Void Deck", 1002, 2, 2, true, true, true, "van hearse", true, true, "Inland Ash Scattering Facility");
                    }
                    else if (PackageId == 2)
                    {
                        generatePackage("Void Deck", 1002, 2, 2, true, true, true, "van hearse", true, true, "Inland Ash Scattering Facility");
                    }
                    else
                    {
                        generatePackage("Void Deck", 1002, 2, 2, true, true, true, "van hearse", true, true, "Inland Ash Scattering Facility");
                    }
                    Boolean flag = _svc.AddFuneral(Funeral, (int)HttpContext.Session.GetInt32("user_id"));
                    return Redirect("/prefuneral/Religion?id=" + Funeral.Id);
                }
            }
            errorMsg = "Please login before attempting to create your funeral package.";
            return Page();
        }

        private void generatePackage(string WakeLocationIn, Int16 CasketID, Int16 WakeDuration, Int16 WakeGuestsExpected, Boolean HasMobileToilet, Boolean HasMakeupServices, Boolean HasHairstylingServices, string FuneralVechicle, Boolean HasFuneralCermony, Boolean HasBusCatering, string FinalRestingPlace)
        {
            Funeral.WakeLocationIn = WakeLocationIn;
            Funeral.CasketID = CasketID;
            Funeral.WakeDuration = WakeDuration;
            Funeral.WakeGuestsExpected = WakeGuestsExpected;
            Funeral.HasMobileToilet = HasMobileToilet;
            Funeral.HasMakeupServices = HasMakeupServices;
            Funeral.HasHairstylingServices = HasHairstylingServices;
            Funeral.FuneralVechicle = FuneralVechicle;
            Funeral.HasFuneralCermony = HasFuneralCermony;
            Funeral.HasBusCatering = HasBusCatering;
            Funeral.FinalRestingPlace = FinalRestingPlace;
        }
    }
}
