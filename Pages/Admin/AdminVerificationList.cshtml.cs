using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using afterlife_caretakers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace afterlife_caretakers.Pages.Admin
{
    public class AdminVerificationListModel : PageModel
    {
        [BindProperty]
        public List<Users> allusers { get; set; }
        [BindProperty]
        public Users MyUsers { get; set; }

        private readonly ILogger<AdminVerificationListModel> _logger;
        private UserService _svc;
        public AdminVerificationListModel(ILogger<AdminVerificationListModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        public void OnGet()
        {
            allusers = _svc.GetAllUsers();
        }
        //public IActionResult OnPost()
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    return RedirectToPage("/Admin/EmailConfirmation_Admin");
        //    //}
        //    //else
        //    //{
        //    //    TempData["AlertMessage"] = "No.";
        //    //    return Page();
        //    //}
        //}
    }
}
