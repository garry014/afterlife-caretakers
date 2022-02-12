using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages.prefuneral
{
    public class Wake_locationModel : PageModel
    {
        private readonly Services.FuneralService _svc;
        public Wake_locationModel(Services.FuneralService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Funeral Funeral { get; set; }
        [BindProperty]
        public FuneralPricing fp { get; set; }
        [BindProperty]
        public string PostalCodeVal { get; set; }

        public IActionResult OnGet(string id)
        {
            // Validate if session exists
            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }

            int x = 0;
            Int32.TryParse(id, out x);
            Funeral = _svc.GetFuneralByFuneralId(x);

            if (Funeral == null)
            {
                return NotFound();
            }

            // Custom validation bypass
            if (Funeral.WakeLocationIn == "SampleText")
            {
                Funeral.WakeLocationIn = "";
            }
            if (Funeral.WakePostalCode == "999999")
            {
                Funeral.WakePostalCode = "";
            }

            if (string.IsNullOrEmpty(Funeral.Religion))
            {
                return Redirect("/prefuneral/religion?id=" + Funeral.Id);
            }

            fp = new FuneralPricing();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Session.GetInt32("user_id") == null)
            {
                return NotFound();
            }
            Funeral.LastUpdatedById = (int)HttpContext.Session.GetInt32("user_id");

            string status = ValidatePostalCode(Funeral.WakePostalCode.ToString());
            if (status == "OK")
            {
                // Google map api return ok
                var included = new[] { "WakeLocationIn", "WakePostalCode" };
                if (_svc.UpdateFuneral(Funeral, included) == true)
                {
                    return Redirect("/prefuneral/wake-plans?id=" + Funeral.Id);
                }
                else
                    return BadRequest();
            }
            else
            {
                if (status == "ZERO_RESULTS")
                {
                    PostalCodeVal = "There's no such postal code found";
                    return Page();
                }
                else
                {
                    PostalCodeVal = "Google Server is currently down, please try again later.";
                    return Page();
                }
            }
            
        }

        public class MyObject
        {
            public string status { get; set; }
        }

        public string ValidatePostalCode(string postal)
        {
            string result = "";

            if (postal.Length != 6)
                return "ZERO_RESULTS";

            System.Diagnostics.Debug.WriteLine(postal);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:" + postal + "|country:SG&key=AIzaSyBO0MrrmlS2Fyp_DWfjlPy_ymuZCHiCqYY");

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        MyObject jsonObject = JsonSerializer.Deserialize<MyObject>(jsonResponse);

                        result = jsonObject.status;
                        System.Diagnostics.Debug.WriteLine(result);
                    }
                }

                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}
