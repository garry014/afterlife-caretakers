using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Casket
    {
        [Required]
        public int id { get; set; }
        [Required, MinLength(3, ErrorMessage ="Enter at least 3 characters.")]
        public string name { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string imageLink { get; set; }
        [Range(0,19999, ErrorMessage ="Enter valid price from 0 to 19999")]
        public float price { get; set; }
        [Required]
        public int selectedTimes { get; set; }
        [Required]
        public Boolean isDeleted { get; set; }
    }
}
