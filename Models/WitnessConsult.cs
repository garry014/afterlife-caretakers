using afterlife_caretakers.CustomValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class WitnessConsult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ServiceType { get; set; }
        public Boolean ServiceStatus { get; set; }

        [Required(ErrorMessage = "Please enter your full name."), Key, MinLength(1, ErrorMessage = "Your full display name is required. Minimum of 1 character.")]
        public string ConsultName { get; set; }

        [Required(ErrorMessage = "Please enter your years of experience."), Range(0, 60, ErrorMessage = "Maximum years of experience is 60. ")]
        public int Experience { get; set; }

        public string ProfileImg { get; set; }
        
        public Boolean PublishStatus { get; set; }

        public int UserId { get; set; }
    }


    public class ImageWitnessConsult
    {
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile ImageC1 { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile ImageC2 { get; set; }
    }
}
