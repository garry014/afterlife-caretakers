using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class BeneficiaryInformation
    {
        //public BeneficiaryInformation(BeneficiaryInformation copy)
        //{
        //    Id = copy.Id;
        //    NAME = copy.NAME;
        //    NRIC = copy.NRIC;
        //    Birthdate = copy.Birthdate;
        //    Relationship = copy.Relationship;
        //    PhoneNo = copy.PhoneNo;
        //}



        //public void ClearData() 
        //{
        //    //Id = 0;
        //    NAME = "";
        //    NRIC = "";
        //    Birthdate = new DateTime();
        //    Relationship = "";
        //    PhoneNo = "";
        //}

        [Required]
        public int Id { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        public string NRIC { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Relationship { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public int OWNERID { get; set; }
    }
}
