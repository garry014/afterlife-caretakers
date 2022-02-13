using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Controllers
{
    public class WillPDFController : Controller
    {
        public IConfiguration Configuration { get; }

        public WillPDFController(IConfiguration Config)
        {
            Configuration = Config;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("~/willpdf")]
        public ActionResult CreateWillPDF()
        {
            var ConnectionString = Configuration.GetConnectionString("MyConn");
            SqlConnection conSelect = new SqlConnection(ConnectionString);
            string sql = "Select column from table"; //change to your columns and table
            SqlCommand selectstatus = new SqlCommand(sql, conSelect);
            conSelect.Open();
            SqlDataReader reader = selectstatus.ExecuteReader();
            reader.Read();
            string name = reader["column"].ToString();

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
            //can text me if you need any help for this part, for the button its in "amdpdf.cshtml"
            //title
            graphics.DrawString("MAKING OF WILL FORM", title, PdfBrushes.Black, new PointF(175, 80));

            //will maker
            graphics.DrawString("PERSON MAKING WILL FORM", subheader, PdfBrushes.Black, new PointF(0, 130));
            graphics.DrawString("Name: " + name, font, PdfBrushes.Black, new PointF(0, 150));
            graphics.DrawString("NRIC: " + name, font, PdfBrushes.Black, new PointF(0, 170));
            graphics.DrawString("Gender: " + name, font, PdfBrushes.Black, new PointF(0, 190));
            //will maker signature
            graphics.DrawString("Signature: " + name, font, PdfBrushes.Black, new PointF(0, 230));

            //witness
            graphics.DrawString("WITNESS OF WILL FORM", subheader, PdfBrushes.Black, new PointF(0, 280));
            graphics.DrawString("Name: " + name, font, PdfBrushes.Black, new PointF(0, 300));
            graphics.DrawString("NRIC: " + name, font, PdfBrushes.Black, new PointF(0, 320));
            //witness signature
            graphics.DrawString("Signature: " + name, font, PdfBrushes.Black, new PointF(0, 360));

            //date
            graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), font, PdfBrushes.Black, new PointF(0, 730));

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();

            document.Save(stream);

            //Set the position as '0'.
            stream.Position = 0;

            //Download the PDF document in the browser
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            fileStreamResult.FileDownloadName = "Will Form.pdf";

            return fileStreamResult;
        }
    }
}
