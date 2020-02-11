using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;

namespace ToolManager.Helpers.UploadHelper
{
    public class UploadHelper
    {
        public static bool IsSingleContentType(IFormFile file)
        {
            return !string.IsNullOrEmpty(file.ContentType)
                   && !file.ContentType.Contains("multipart/");
        }
        public static bool IsInLengthLimits(IFormFile file, int lengthLimitMax, int lengthLimitMinIncluded = 0)
        {
            if (file.Length > lengthLimitMax)
                return false;
            else if (file.Length <= lengthLimitMinIncluded)
                return false;

            return true;
        }
        public static bool HasAValidExtention(IFormFile file, List<string> extentionsList)
        {
            if (!extentionsList.Contains(Path.GetExtension(file.FileName)))
                return false;

            return true;
        }
    }
}
