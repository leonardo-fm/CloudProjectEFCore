using CloudProjectCore.Models.BlobStorage;
using CloudProjectCore.Models.MongoDB;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToolManager;

namespace CloudProjectCore.Models.DeleteUserData
{
    public class UserData
    {
        private string _userId;

        public UserData(string userId)
        {
            _userId = userId;
        }

        public async void DeleteUserData()
        {
            using (var myMongoDBManager = 
                new MyMongoDBManager(Variables.MongoDBConnectionStringRW, Variables.MongoDBDatbaseName))
            using (var myBlobStorageManager = 
                new MyBlobStorageManager(Variables.BlobStorageConnectionString, _userId))
            {
                var response = await myBlobStorageManager.DeleteUserContainerAsync();

                if (response.IsSuccess)
                {
                    var photos = await myMongoDBManager.GetPhotoForGalleryAsync(_userId);
                    foreach (var photo in photos)
                    {
                        myMongoDBManager.RemovePhotoAsync(photo._id);
                    }
                }
            }
        }
    }
}
