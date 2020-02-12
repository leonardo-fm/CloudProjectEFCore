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
        public List<PhotoModelForGallery> photos { get; set; }
        public bool multipleDeletes { get; set; }
        public string lastTag { get; set; }
    }
}
