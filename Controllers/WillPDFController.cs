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
            SqlConnection conSelectBene = new SqlConnection(ConnectionString);
            //bene
            string benesql = "Select NAME,BirthDate,RelationShip,PhoneNo from Beneficiary"; //change to your columns and table
            SqlCommand selectstatus = new SqlCommand(benesql, conSelectBene);
            conSelectBene.Open();
            SqlDataReader reader = selectstatus.ExecuteReader();
            reader.Read();
            string name = reader["NAME"].ToString();
            string bd = reader["BirthDate"].ToString();
            string rel = reader["RelationShip"].ToString();
            string phoneno = reader["PhoneNo"].ToString();

            //Will Maker

            SqlConnection conSelect = new SqlConnection(ConnectionString);
            //string usersql = "Select name, NRIC, address, postal, phoneno from Users";
            string usersql = "Select name, NRIC, phoneno from Users";
            SqlCommand selectuser = new SqlCommand(usersql, conSelect);
            conSelect.Open();
            SqlDataReader reader2 = selectuser.ExecuteReader();
            reader2.Read();

            string uname = reader2["name"].ToString();
            string unric = reader2["NRIC"].ToString();
            //string ugender = reader2["gender"].ToString();
            //string udob = reader2["dob"].ToString();
            //string uaddress = reader2["address"].ToString();
            //string upostal = reader2["postal"].ToString();
            string uhomeno = reader2["phoneno"].ToString();

            //executor
            SqlConnection conSelectExec = new SqlConnection(ConnectionString);
            string execsql = "Select NAME, NRIC, EMAIL, PhoneNo from Executor";
            SqlCommand selectexec = new SqlCommand(execsql, conSelectExec);
            conSelectExec.Open();
            SqlDataReader reader3 = selectexec.ExecuteReader();
            reader3.Read();
            string ename = reader3["NAME"].ToString();
            string enric = reader3["NRIC"].ToString();
            string eemail = reader3["EMAIL"].ToString();
            string ePhoneNo = reader3["PhoneNo"].ToString();

            //witness
            SqlConnection conSelectW = new SqlConnection(ConnectionString);
            string witnesssql = "Select Relationship,NAME,NRIC, PhoneNo, Email from Witness";
            SqlCommand selectwitness = new SqlCommand(witnesssql, conSelectW);
            conSelectW.Open();
            SqlDataReader reader4 = selectwitness.ExecuteReader();
            reader4.Read();
            string wrel = reader4["Relationship"].ToString();
            string wname = reader4["NAME"].ToString();
            string wnric = reader4["NRIC"].ToString();
            string wphoneno = reader4["PhoneNo"].ToString();
            string wemail = reader4["Email"].ToString();

            //asset
            SqlConnection conSelectAsset = new SqlConnection(ConnectionString);
            string assetsql = "Select gift_type, description from Asset";
            SqlCommand selectasset = new SqlCommand(assetsql, conSelectAsset);
            conSelectAsset.Open();
            SqlDataReader reader5 = selectasset.ExecuteReader();
            reader5.Read();
            string gifttype = reader5["gift_type"].ToString();
            string desc = reader5["description"].ToString();

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

            //WILL maker
            graphics.DrawString("PERSON MAKING THE WILL FORM", subheader, PdfBrushes.Black, new PointF(0, 130));
            graphics.DrawString("Name: " + uname, font, PdfBrushes.Black, new PointF(0, 150));
            graphics.DrawString("NRIC: " + unric, font, PdfBrushes.Black, new PointF(0, 170));
            //graphics.DrawString("Gender: " + ugender, font, PdfBrushes.Black, new PointF(0, 190));
            //graphics.DrawString("Date of Birth: " + udob, font, PdfBrushes.Black, new PointF(0, 210));
            //graphics.DrawString("Address: " + uaddress, font, PdfBrushes.Black, new PointF(0, 230));
            //graphics.DrawString("Postal Code: " + upostal, font, PdfBrushes.Black, new PointF(0, 250));
            graphics.DrawString("Home Number: " + uhomeno, font, PdfBrushes.Black, new PointF(0, 190));
            //will maker signature
            graphics.DrawString("Signature: " + unric, font, PdfBrushes.Black, new PointF(0, 330));

            //executor 280
            graphics.DrawString("EXECUTOR OF WILL FORM", subheader, PdfBrushes.Black, new PointF(0, 240));
            graphics.DrawString("Name: " + ename, font, PdfBrushes.Black, new PointF(0, 260));
            graphics.DrawString("NRIC: " + enric, font, PdfBrushes.Black, new PointF(0, 280));
            graphics.DrawString("Email: " + eemail, font, PdfBrushes.Black, new PointF(0, 300));
            graphics.DrawString("Phone No: " + ePhoneNo, font, PdfBrushes.Black, new PointF(0, 320));
            //witness
            graphics.DrawString("WITNESS OF WILL FORM", subheader, PdfBrushes.Black, new PointF(0, 370));
            graphics.DrawString("Name: " + wname, font, PdfBrushes.Black, new PointF(0, 390));
            graphics.DrawString("NRIC: " + wnric, font, PdfBrushes.Black, new PointF(0, 410));
            graphics.DrawString("Email: " + wemail, font, PdfBrushes.Black, new PointF(0, 430));
            graphics.DrawString("Phone No: " + wphoneno, font, PdfBrushes.Black, new PointF(0, 450));
            graphics.DrawString("Relationship to WillMaker: " + wrel, font, PdfBrushes.Black, new PointF(0, 470));
            //beneficiary 
            graphics.DrawString("BENEFICIARY OF WILL FORM", subheader, PdfBrushes.Black, new PointF(0, 520));
            graphics.DrawString("Name: " + name, font, PdfBrushes.Black, new PointF(0, 540));
            graphics.DrawString("BirthDate: " + bd, font, PdfBrushes.Black, new PointF(0, 560));
            graphics.DrawString("Relationship to WillMaker: " + rel, font, PdfBrushes.Black, new PointF(0, 580));
            graphics.DrawString("Phone Number: " + phoneno, font, PdfBrushes.Black, new PointF(0, 600));
            //asset
            graphics.DrawString("WILLMAKER LISTED ASSETS", subheader, PdfBrushes.Black, new PointF(0, 650));
            graphics.DrawString("Gift Type: " + gifttype, font, PdfBrushes.Black, new PointF(0, 670));
            graphics.DrawString("Description: " + desc, font, PdfBrushes.Black, new PointF(0, 690));

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
