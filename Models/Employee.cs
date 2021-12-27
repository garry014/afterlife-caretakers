using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pract2.Models
{
    public class Employee
    {
        [Required,Key, MinLength(3,ErrorMessage ="Enter at least 3 characters"), 
         MaxLength(5)]
 
        public string Id { get; set; }

        [Required]
        public string NRIC { get; set; }

        [Required, MaxLength(25)]
         public string Name { get; set; }
        
        [Required]
        public string Gender { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
       
      
        [Range(1500,9000, ErrorMessage="Enter valid salary from 1500 to 9000")]
        public Double Salary { get; set; }
        
        [Required]
        public String  Department { get; set; }
    }

}
