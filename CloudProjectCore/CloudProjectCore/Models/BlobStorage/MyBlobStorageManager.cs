using System;
using ToolManager.BlobStorage;

namespace CloudProjectCore.Models.BlobStorage
{
    public class MyBlobStorageManager : BlobStorageManager
    {
        public MyBlobStorageManager(string connectionString, string userId) : base(connectionString, userId) { }

        public string GetLinkForSharePhoto(DateTime expireDate)
        {
            int totalNumberOfMinutesToAdd = (int)(expireDate - DateTime.Now).TotalMinutes;
            return GetContainerSasUri(totalNumberOfMinutesToAdd);
        }
    }
}
