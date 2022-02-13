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

namespace afterlife_caretakers.Pages.Admin
{
    public class createconsultModel : PageModel
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly Services.ConsultService _svc;
        //private readonly Services.UserService _usvc;
        private readonly Services.AdminService _asvc;
        public createconsultModel( IWebHostEnvironment hostEnvironment, Services.ConsultService service, Services.AdminService aservice)
        {
            
            webHostEnvironment = hostEnvironment;
            _svc = service;
            //_usvc = uservice;
            _asvc = aservice;
        }

        [BindProperty]
        public ConsultProfile Consult { get; set; }
        

        [BindProperty]
        public Admins Admin { get; set; }

        [BindProperty]
        public List<Admins> Admins{get; set;}

        [BindProperty]
        public ImageConsult ImageC { get; set; }

        [BindProperty]
        public string ValidateMsg { get; set; }




        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                // return NotFound(); //not loggeed in 
                return RedirectToPage("/Admin/AdminLogin");
            }
            
            int userid = 0;
            userid = (int)HttpContext.Session.GetInt32("user_id");

            if (userid != 0)
            {
                Admin = _asvc.GetAdminByID(userid);
                //Admins = _asvc.GetAllAdmin();

                Consult = _svc.GetConsultByUserId(userid);
                if(Consult != null)
                {
                    return Redirect("/Admin/editconsult/" + Consult.Id);
                }
                System.Diagnostics.Debug.WriteLine("id and name: " + Admin.Id +" "+ Admin.name);
                
        
                /////////////////trying to redirect//////////////////////
                //allconsults = _svc.GetAllConsults();
                //if (userid == allconsults.Consult.UserId)
                //{
                //    return Page();
                //}
                //else
                //{
                //    return RedirectToPage("/team/editconsult/" + Consult.Id);
                //}

            }
            return Page();

        }

        public IActionResult OnPost(ImageConsult ImageC)
        {
            //System.Diagnostics.Debug.WriteLine(Consult.Id);
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(ImageC);
                Consult.ProfileImg = uniqueFileName;
                if (_svc.AddConsult(Consult))
                {
                    // Create session
                    System.Diagnostics.Debug.WriteLine("hereright"+Consult.Id);
                    //redirect?????????????????????????????????????????????
                    return Redirect("/Admin/editconsult/" + Consult.Id);
                    
                }
            }
            System.Diagnostics.Debug.WriteLine("here?");
            return Page();
        }

        private string UploadedFile(ImageConsult ImageC)
        {
            string uniqueFileName = null;

            if (ImageC != null)
            {
                System.Diagnostics.Debug.WriteLine("hello " + ImageC);
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/profiles/counselling/");
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
