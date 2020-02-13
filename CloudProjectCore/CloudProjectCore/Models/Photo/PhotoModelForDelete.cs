using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudProjectCore.Models.Photo
{
    public class PhotoModelForDelete
    {
        public string _id { get; set; }
        public bool toBeDelete { get; set; }
    }
}
