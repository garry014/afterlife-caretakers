using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace afterlife_caretakers.Controllers
{
    public class SignatureController : Controller
    {
        //To use
        //1. Before redirecting here, store a record of your feature name FROM your feature (so can bring back to your feature)
        //HttpContext.Session.SetString("SignatureReferral", "funeral");

        //2. Redirect FROM your feature to this route - MUST use localredirect
        //return LocalRedirect("/Video");

        //3. Inside /Pages/SignatureRoute.cs > put in your own else if loop to direct back to your route

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> get_signature(string base64png)
        {
            var dataUri = base64png;//"data:image/png;base64,iVBORw0K...";
            if (!string.IsNullOrEmpty(dataUri))
            {
                var encodedImage = dataUri.Split(',')[1];
                var decodedImage = Convert.FromBase64String(encodedImage);
                string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "signature");
                string UniqueFileName = Guid.NewGuid().ToString() + ".png";
                string UploadPath = Path.Combine(UploadFolder, UniqueFileName);
                await System.IO.File.WriteAllBytesAsync(UploadPath, decodedImage);
                HttpContext.Session.SetString("ESignatureFile", UniqueFileName);
            }
            return Json(HttpStatusCode.OK);
        }
    }
}
