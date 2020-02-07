using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ToolManager.BlobStorage
{
    public class BlobStorageManager : Responses
    {
        private readonly string _connectionString;
        private CloudBlobClient _blobConnection;
        private CloudBlobContainer _blobUserContainer;

        public BlobStorageManager(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _blobUserContainer = SelectContainer(containerName);
            BlocConnection();
        }
        public BlobStorageManager(string defaultEndpointsProtocol, string accountName, string accountKey, string EndpointSuffix, string containerName)
        {
            _connectionString = GetConnectionString(defaultEndpointsProtocol, accountName, accountKey, EndpointSuffix);
            _blobUserContainer = SelectContainer(containerName);
            BlocConnection();
        }

        public async Task<Responses> AddDocument(Stream document, string documentName)
        {
            try
            {
                CloudBlockBlob cBlob = _blobUserContainer.GetBlockBlobReference(documentName);
                await cBlob.UploadFromStreamAsync(document);
                return new Responses(true, false, false);
            }
            catch (Exception)
            {
                return new Responses(false, true, false);
            }
        }
        public async Task<Responses> RemoveDocumentByName(string userDocument)
        {
            try
            {
                CloudBlockBlob cBlob = _blobUserContainer.GetBlockBlobReference(userDocument);
                var response = await cBlob.DeleteIfExistsAsync();
                if(response)
                    return new Responses(true, false, false);
                return new Responses(false, true, false);
            }
            catch (Exception)
            {
                return new Responses(false, true, false);
            }
        }
        public string GetContainerSasUri(int minutesToAdd = 1)
        {
            if (minutesToAdd <= 0)
                minutesToAdd = 1;

            string sasContainerToken;
            int timeDifferencesInMinutes = (DateTime.Now.Hour - DateTime.UtcNow.Hour) * 60;

            SharedAccessBlobPolicy adHocPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(minutesToAdd + timeDifferencesInMinutes),
                Permissions = SharedAccessBlobPermissions.Read
            };

            sasContainerToken = _blobUserContainer.GetSharedAccessSignature(adHocPolicy, null);

            return sasContainerToken;
        }
        public async Task<Responses> DeleteUserContainer()
        {
            try
            {
                var response = await _blobUserContainer.DeleteIfExistsAsync();
                if (response)
                    return new Responses(true, false, false);
                return new Responses(false, true, false);
            }
            catch (Exception)
            {
                return new Responses(false, true, false);
            }
        }
        
        private string GetConnectionString(string defaultEndpointsProtocol, string accountName, string accountKey, string EndpointSuffix)
        {
            return defaultEndpointsProtocol + accountName + accountKey + EndpointSuffix;
        }
        private void BlocConnection()
        {
            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(_connectionString);
                _blobConnection = account.CreateCloudBlobClient();
            }
            catch (Exception)
            {
                throw new Exception("Wrong connection string");
            }
        }
        private CloudBlobContainer SelectContainer(string userId)
        {
            var userContainer = _blobConnection.GetContainerReference(userId);
            userContainer.CreateIfNotExistsAsync().Wait();

            return userContainer;
        }
    }
}
