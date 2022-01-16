using afterlife_caretakers.CustomValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Casket
    {
        public int Id { get; set; }
        [Required, MinLength(3, ErrorMessage ="Enter at least 3 characters.")]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        public string ImageLink { get; set; }

        [Range(0,19999, ErrorMessage ="Enter valid price from 0 to 19999")]
        public double Price { get; set; }
        public Int16 SelectedTimes { get; set; }
        
        [Required]
        public Boolean IsDeleted { get; set; }
    }

    public class ImageClass
    {
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Image { get; set; }
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Image2 { get; set;}
    }

    public enum Category
    {
        Casket, Urn
    }
}
