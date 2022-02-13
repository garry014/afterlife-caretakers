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
    public class VideoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecoredFile()
        {
            if (Request.Form.Files.Any())
            {
                var file = Request.Form.Files["video-blob"];
                string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                string UniqueFileName = Guid.NewGuid().ToString() + ".webm";
                string UploadPath = Path.Combine(UploadFolder, UniqueFileName);
                await file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
                System.Diagnostics.Debug.WriteLine("done");
                // Store the path in a session, and JS will move to next razor page.
                HttpContext.Session.SetString("VideoName", UniqueFileName);
            }
            return Json(HttpStatusCode.OK);
        }
    }
}
