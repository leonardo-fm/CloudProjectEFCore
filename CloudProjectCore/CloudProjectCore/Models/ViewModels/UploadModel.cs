using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CloudProjectCore.Models.ViewModels
{
    public class UploadModel
    {
        public string Message { get; set; }

        [Display(Name = "File")]
        public IFormFile File { get; set; }
    }
}
