using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.Photo
{
    public class PhotoModelForMap
    {
        public string _id { get; set; }
        public string IconPath { get; set; }
        public string PhotoName { get; set; }
        public double PhotoLatitude { get; set; }
        public double PhotoLongitude { get; set; }
    }
}
