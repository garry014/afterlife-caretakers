using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages
{
    public class SignatureRouteModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public SignatureRouteModel(Services.FuneralService service, IWebHostEnvironment hostEnvironment)
        {
            _svc = service;
            webHostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment webHostEnvironment;
        public Funeral Funeral { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ESignatureFile")))
            {
                return NotFound();
            }

            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SignatureRedirectBack")))
            {
                return NotFound();
            }
            else
            {
                // Optional if you want to update signature into db
                if (HttpContext.Session.GetString("SignatureRedirectBack").Contains("funeral"))
                {
                    int x = 0;
                    string[] linkList = HttpContext.Session.GetString("SignatureRedirectBack").Split("?id=");
                    Int32.TryParse(linkList[1], out x);

                    //Funeral Funeral = _svc.GetFuneralByFuneralId(x);

                    var included = new[] { "Signature" };
                    Funeral Funeral = new Funeral();
                    Funeral.Id = x;
                    Funeral.Signature = HttpContext.Session.GetString("ESignatureFile").ToString();

                    if (_svc.UpdateFuneral(Funeral, included) == true)
                    {
                        return Redirect(HttpContext.Session.GetString("SignatureRedirectBack"));
                    }
                    else
                        return BadRequest();
                }
                else
                {
                    // DO NOT CHANGE THIS
                    return Redirect(HttpContext.Session.GetString("SignatureRedirectBack"));
                }
                
            }
            return NotFound();
        }
    }
}
