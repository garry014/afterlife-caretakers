using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Admins
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string admin_role { get; set; }
        [MaxLength(8)]
        public string office_num { get; set; }

        public string specialisation { get; set; }
        public string status { get; set; }
        public string clinic_address { get; set; }
    
        public string creationuser { get; set; }
    }
}
