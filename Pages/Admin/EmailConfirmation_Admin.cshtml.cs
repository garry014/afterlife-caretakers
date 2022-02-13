using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace afterlife_caretakers.Pages.Admin
{
    public class EmailConfirmation_AdminModel : PageModel
    {
        private readonly UserService _svc;
        public EmailConfirmation_AdminModel(UserService service)
        {
            _svc = service;
        }

        [BindProperty]
        public Users MyUser { get; set; }

        public void OnGet()
        {
            
            //Console.WriteLine(MyUser.email + "emaillltest");
            SendEmail().Wait();
            
        }

        public async Task SendEmail()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("sekkiyukine1000@gmail.com", "ALCaretakers");
            var subject = "Account has been activated";
            var to = new EmailAddress("shuxian1000@gmail.com", "Amelia Tan");
            var plainTextContent = "You can now access your account!";
            var htmlContent = "<strong>Your Account is ready for use.</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
