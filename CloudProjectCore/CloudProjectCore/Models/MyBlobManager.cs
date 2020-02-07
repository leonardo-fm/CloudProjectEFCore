using System;
using ToolManager.BlobStorage;

namespace CloudProjectCore.Models
{
    public class MyBlobManager : BlobStorageManager
    {
        public MyBlobManager(string connectionString, string userId) : base(connectionString, userId) { }

        public string GetLinkForSharePhoto(DateTime expireDate)
        {
            int totalNumberOfMinutesToAdd = (int)(expireDate - DateTime.Now).TotalMinutes;
            return GetContainerSasUri(totalNumberOfMinutesToAdd);
        }
    }
}
