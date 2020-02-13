using CloudProjectCore.Models.MongoDB;
using CloudProjectCore.Models.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.ViewModels
{
    public class GalleryModel
    {
        public List<PhotoModelForGallery> Photos { get; set; }
        public bool MultipleDeletes { get; set; }
        public string LastTag { get; set; }
    }
}
