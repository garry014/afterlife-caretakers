using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace afterlife_caretakers.Models
{
    public class AMDWitness
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Your name is required")]
        public String name { get; set; }
        [Required(ErrorMessage = "Your NRIC is required")]
        public String nric { get; set; }
        //[Required]
        //public String gender { get; set; }
        //[DataType(DataType.Date)] 
        //public DateTime dob { get; set; }
        [Required(ErrorMessage = "Your address is required")]
        public String address { get; set; }
        [Required, MaxLength(6), MinLength(6, ErrorMessage ="Postal code has to be 6 digits")]
        public String postal { get; set; }
        [Required(ErrorMessage = "Your home number is required"), MaxLength(8)]
        public String homeno { get; set; }
        [Required(ErrorMessage = "Your office number is required"), MaxLength(8)]
        public String officeno { get; set; }
    }
}
