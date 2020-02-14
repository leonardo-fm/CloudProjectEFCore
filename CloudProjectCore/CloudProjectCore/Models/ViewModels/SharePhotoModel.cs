using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.ViewModels
{
    public class SharePhotoModel
    {
        public string _id { get; set; }
        public string PhotoUri { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of expire")]
        public DateTime DateOfExpire { get; set; }

        public string UriForSheredImage { get; set; }
    }
}
