using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    //MaxLength(25)]
    public class MaritalInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required, MaxLength(9)]
        public string NRIC { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required, MaxLength(8)]
        public string PhoneNo { get; set; }
        [Required]
        public string Mstatus { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int OWNERID { get; set; }
    }
}
