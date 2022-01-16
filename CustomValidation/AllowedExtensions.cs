using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.CustomValidation
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensions(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            string allowedExtensions = "";
            for (var i=0; i <_extensions.Length; i++)
            {
                allowedExtensions = allowedExtensions + _extensions[i];
                if (i+1 < _extensions.Length)
                {
                    allowedExtensions = allowedExtensions + ", ";
                }
            }
            return $"Only " + allowedExtensions + " file type is allowed.";
        }
    }
}
