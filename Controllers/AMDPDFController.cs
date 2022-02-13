using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.Drawing;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Services;
using Microsoft.Extensions.Logging;
using afterlife_caretakers.Pages.amd;
using afterlife_caretakers.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace afterlife_caretakers.Controllers
{
    public class AMDPDFController : Controller
    {
        public IConfiguration Configuration { get; }

        public AMDPDFController(IConfiguration Config)
        {
            Configuration = Config;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("~/amdpdf")]
        public ActionResult CreateAMDPDF()
        {
            var ConnectionString = Configuration.GetConnectionString("MyConn");
            SqlConnection conSelectw= new SqlConnection(ConnectionString);
            string witnesssql = "Select name, nric, address, postal, homeno, officeno from amdwitness";
            SqlCommand selectw = new SqlCommand(witnesssql, conSelectw);
            conSelectw.Open();
            SqlDataReader reader = selectw.ExecuteReader();
            reader.Read();
            string wname = reader["name"].ToString();
            string wnric = reader["nric"].ToString();
            string waddress = reader["address"].ToString();
            string wpostal = reader["postal"].ToString();
            string whomeno = reader["homeno"].ToString();
            string wofficeno = reader["officeno"].ToString();  
            
            //Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Load the image as stream.
            string ImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo.png");
            FileStream imageStream = new FileStream(ImageFolder, FileMode.Open, FileAccess.Read);
            PdfBitmap image = new PdfBitmap(imageStream);

            //Draw the image
            graphics.DrawImage(image, 0, 0, 188, 50);

            //Set the standard font
            PdfFont title = new PdfStandardFont(PdfFontFamily.Helvetica, 15, PdfFontStyle.Bold);
            PdfFont subheader = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold | PdfFontStyle.Underline);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            //Draw the text
            //title
            graphics.DrawString("MAKING OF ADVANCE MEDICAL DIRECTIVE", title, PdfBrushes.Black, new PointF(100, 80));
            
            //amd maker
            graphics.DrawString("PERSON MAKING THE ADVANCE MEDICAL DIRECTIVE", subheader, PdfBrushes.Black, new PointF(0, 130));
            graphics.DrawString("Name: Nina Tan", font, PdfBrushes.Black, new PointF(0, 150));
            graphics.DrawString("NRIC: S1234567A", font, PdfBrushes.Black, new PointF(0, 170));
            graphics.DrawString("Gender: Female", font, PdfBrushes.Black, new PointF(0, 190));
            graphics.DrawString("Date of Birth: 08/15/1950", font, PdfBrushes.Black, new PointF(0, 210));
            graphics.DrawString("Address: Yishun Ave 2, Blk 700", font, PdfBrushes.Black, new PointF(0, 230));
            graphics.DrawString("Postal Code: Singapore 769098", font, PdfBrushes.Black, new PointF(0, 250));
            graphics.DrawString("Home Number: 65985462", font, PdfBrushes.Black, new PointF(0, 270));
            graphics.DrawString("Office Number: 63663248", font, PdfBrushes.Black, new PointF(0, 290));

            //witness
            graphics.DrawString("WITNESS OF THE ADVANCE MEDICAL DIRECTIVE", subheader, PdfBrushes.Black, new PointF(0, 340));
            graphics.DrawString("Name: "+wname, font, PdfBrushes.Black, new PointF(0, 360));
            graphics.DrawString("NRIC: "+wnric, font, PdfBrushes.Black, new PointF(0, 380));
            graphics.DrawString("Home Address: "+waddress, font, PdfBrushes.Black, new PointF(0, 400));
            graphics.DrawString("Postal Code: " + wpostal, font, PdfBrushes.Black, new PointF(0, 420));
            graphics.DrawString("Home Number: "+whomeno, font, PdfBrushes.Black, new PointF(0, 440));
            graphics.DrawString("Office Number: "+wofficeno, font, PdfBrushes.Black, new PointF(0, 560));
            
            graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), font, PdfBrushes.Black, new PointF(0, 730));

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            //Set the position as '0'.
            stream.Position = 0;

            //Download the PDF document in the browser
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            fileStreamResult.FileDownloadName = "AMD Form.pdf";

            return fileStreamResult;
        }
    }
}
