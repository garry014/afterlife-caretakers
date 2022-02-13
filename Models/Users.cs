using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Users
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required, MaxLength(8)]
        public string phoneno { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string gender { get; set; }
       
        public string usertype { get; set; }
        [Required, MaxLength(9)]
        public string NRIC { get; set; }

        public string willformID { get; set; }
        
        public string NRIC_upload { get; set; }

        public string activation_status { get; set; }
       
        public string deathcert_upload { get; set; }
        
        public string deathdate_setting { get; set; }
        
        public string address { get; set; }
    }
}
