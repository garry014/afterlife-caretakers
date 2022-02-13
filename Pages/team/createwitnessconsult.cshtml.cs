using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.team
{
    public class createwitnessconsultModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly Services.WitnessService _wsvc;
        private readonly Services.UserService _usvc;
        private readonly Services.AdminService _asvc;
        public createwitnessconsultModel(IWebHostEnvironment hostEnvironment, Services.WitnessService service, Services.UserService uservice, Services.AdminService aservice)
        {

            webHostEnvironment = hostEnvironment;
            _wsvc = service;
            _usvc = uservice;
            _asvc = aservice;
        }

        //[BindProperty]
        //public ConsultProfile Consult { get; set; }
        [BindProperty]
        public Users User { get; set; }

        [BindProperty]
        public WitnessConsult Witness { get; set; }

        [BindProperty]
        public List<Admins> Admins { get; set; }

        [BindProperty]
        public ImageConsult ImageC { get; set; }

        [BindProperty]
        public string ValidateMsg { get; set; }




        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                // return NotFound(); //not loggeed in 
                return RedirectToPage("../Main_Login");
            }

            int userid = 0;
            userid = (int)HttpContext.Session.GetInt32("user_id");

            if (userid != 0)
            {
                User = _usvc.GetUserByID(userid);
                //Admins = _asvc.GetAllAdmin();

                Witness = _wsvc.GetWitConsultByUserId(userid);
                if (Witness != null)
                {
                    return Redirect("/team/editwitnessconsult/" + Witness.Id);
                }
                System.Diagnostics.Debug.WriteLine("id and name: " + User.Id + " " + User.name);


            }
            return Page();

        }

        public IActionResult OnPost(ImageWitnessConsult ImageC)
        {
            //System.Diagnostics.Debug.WriteLine(Consult.Id);
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(ImageC);
                Witness.ProfileImg = uniqueFileName;
                if (_wsvc.AddWitConsult(Witness))
                {
                    // Create session
                    System.Diagnostics.Debug.WriteLine("hereright" + Witness.Id);
                    //redirect?????????????????????????????????????????????
                    return Redirect("/team/editwitnessconsult/" + Witness.Id);

                }
            }
            System.Diagnostics.Debug.WriteLine("here?");
            return Page();
        }

        private string UploadedFile(ImageWitnessConsult ImageC)
        {
            string uniqueFileName = null;

            if (ImageC != null)
            {
                System.Diagnostics.Debug.WriteLine("hello " + ImageC);
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/profiles/witness/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageC.ImageC1.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageC.ImageC1.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
