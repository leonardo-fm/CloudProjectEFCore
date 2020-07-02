using CloudProjectCore.Models.Photo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace CloudProjectCore.Models.Upload
{
    public class ExifDataExtractor
    {
        public PhotoResponseForExif GetExifDataFromImage(Image photo)
        {
            PhotoResponseForExif responseForExif = new PhotoResponseForExif();

            PropertyItem[] exifArray = photo.PropertyItems;

            if (exifArray.Length == 0)
                return responseForExif;

            Dictionary<int, byte[]> exifDictionary =
                exifArray.ToDictionary(x => x.Id, x => x.Value != null ? x.Value : new byte[] { });

            responseForExif.PhotoTagImageWidth = photo.Width.ToString();
            responseForExif.PhotoTagImageHeight = photo.Height.ToString();

            responseForExif.PhotoGpsLatitude = exifDictionary.ContainsKey(0x0002)
                ? (double?)GetGPSValues(exifDictionary[0x0002])
                : null;

            responseForExif.PhotoGpsLongitude = exifDictionary.ContainsKey(0x0004)
                ? (double?)GetGPSValues(exifDictionary[0x0004])
                : null;

            responseForExif.PhotoTagDateTime = exifDictionary.ContainsKey(0x0132)
                ? Encoding.UTF8.GetString(exifDictionary[0x0132]).Replace("\0", "")
                : "";

            responseForExif.PhotoTagThumbnailEquipModel =
                exifDictionary.ContainsKey(0x010F) && exifDictionary.ContainsKey(0x0110)
                ? Encoding.UTF8.GetString(exifDictionary[0x010F]).Replace("\0", "")
                + "/"
                + Encoding.UTF8.GetString(exifDictionary[0x0110]).Replace("\0", "")
                : "";

            return responseForExif;
        }

        private double GetGPSValues(byte[] value)
        {
            byte[] degrees1 = new byte[] { value[0], value[1], value[2], value[3] };
            byte[] degrees2 = new byte[] { value[4], value[5], value[6], value[7] };

            byte[] first1 = new byte[] { value[8], value[9], value[10], value[11] };
            byte[] first2 = new byte[] { value[12], value[13], value[14], value[15] };

            byte[] second1 = new byte[] { value[16], value[17], value[18], value[19] };
            byte[] second2 = new byte[] { value[20], value[21], value[22], value[23] };

            double degrees = (double)BitConverter.ToInt32(degrees1, 0) / BitConverter.ToInt32(degrees2, 0);
            double firsts = (double)BitConverter.ToInt32(first1, 0) / BitConverter.ToInt32(first2, 0);
            double seconds = (double)BitConverter.ToInt32(second1, 0) / BitConverter.ToInt32(second2, 0);

            return Math.Round(degrees + (firsts / 60) + (seconds / 3600), 5);
        }
    }
}
