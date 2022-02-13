using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class WitnessInformation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Relationship { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        public string NRIC { get; set; }
        //[Required] birthdate would be checked in beneficiary side if their qualified.
        //public DateTime Birthdate { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int OWNERID { get; set; }
    }
}
