using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.Photo
{
    public class PhotoResponseForExif
    {
        public double? PhotoGpsLatitude { get; set; }
        public double? PhotoGpsLongitude { get; set; }
        public string PhotoTagDateTime { get; set; }
        public string PhotoTagImageWidth { get; set; }
        public string PhotoTagImageHeight { get; set; }
        public string PhotoTagThumbnailEquipModel { get; set; }
    }
}
