using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class MaritalInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        public string NRIC { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Mstatus { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int OWNERID { get; set; }
    }
}
