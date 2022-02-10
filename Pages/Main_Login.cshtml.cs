using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using afterlife_caretakers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace afterlife_caretakers.Pages
{
  
    public class Main_LoginModel : PageModel
    {
        [BindProperty]
        public Beneficiary Credential { get; set; }
        
        public void OnGet()
        {
            //this.Credential = new Beneficiary { email = "shuxian1000@gmail.com" };
        }

        public void OnPost()
        {

        }
    }

    public class Credential
    {
        [Required]
      
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

  
}
