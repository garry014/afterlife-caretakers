using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class PersonalInformation
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(25)]
        public string Name { get; set; }

        [Required]

        public string NRIC { get; set; }


        //[DataType(DataType.Date)]
        //public DateTime BirthDate { get; set; }

        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string HomeAddr { get; set; }


        [Required]
        public string Gender { get; set; }


        [Required]
        public int OWNERID { get; set; }

    }
}
