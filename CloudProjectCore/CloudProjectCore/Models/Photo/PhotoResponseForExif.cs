using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.Photo
{
    public class PhotoResponseForExif
    {
        public double? photoGpsLatitude { get; set; }
        public double? photoGpsLongitude { get; set; }
        public string photoTagDateTime { get; set; }
        public string photoTagImageWidth { get; set; }
        public string photoTagImageHeight { get; set; }
        public string photoTagThumbnailEquipModel { get; set; }
    }
}
