using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Thitipong.Upload.Web.Models
{
    public class FilePost
    {

        [MaxFileSize(1048576)]
        [AllowedExtensions(new string[] { ".csv" , ".xml" })]
        public IFormFile MyFile { set; get; }


    }


    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly string _errMsg;

        public MaxFileSizeAttribute(int maxFileSize,string errMsg = null)
        {
            _maxFileSize = maxFileSize;
            _errMsg = errMsg;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            if (_errMsg != null)
            {
                return _errMsg;
            }
            return $"Maximum allowed file size is { _maxFileSize} bytes.";
        }
    }


    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly string _errMsg;
        public AllowedExtensionsAttribute(string[] extensions, string errMsg = null)
        {
            _extensions = extensions;
            _errMsg = errMsg;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
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
            if (_errMsg != null)
            {
                return _errMsg;
            }

            return "Unknown format";
        }
    }
}
