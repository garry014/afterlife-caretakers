using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Gift
    { 
        [Required]
        public int Id { get; set; }
        [Required]
        public string TYPE { get; set; }
        [Required]
        public string gift_type { get; set; }
        [Required]
        //reason as to why theres no required is cuz, its dependent on the user choice.
        //public int Amount { get; set; }
        //public string Address { get; set; }
        //public string BankName { get; set; }
        //public string BankAccount { get; set; }
        public string description { get; set; }
        [Required]
        public int OWNERID { get; set; }

        [Required]
        public int BeneID { get; set; }
        //non-specific gift types just chooses which beneficiary to give them etc. by default == real estate?

    }
}
