using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ToolManager.BlobStorage
{
    public class BlobStorageManager : Responses, IDisposable
    {
        private CloudBlobClient _blobConnection;
        private CloudBlobContainer _blobUserContainer;

        public BlobStorageManager(string connectionString, string containerName)
        {
            _blobConnection = BlocConnection(connectionString);
            SelectContainer(containerName);
        }
        public BlobStorageManager(string defaultEndpointsProtocol, string accountName, string accountKey, string EndpointSuffix, string containerName)
        {
            var connectionString = GetConnectionString(defaultEndpointsProtocol, accountName, accountKey, EndpointSuffix);
            _blobConnection = BlocConnection(connectionString);
            SelectContainer(containerName);
        }

        public async Task<Responses> AddDocumentAsync(Stream document, string documentName)
        {
            try
            {
                CloudBlockBlob cBlob = _blobUserContainer.GetBlockBlobReference(documentName);
                await cBlob.UploadFromStreamAsync(document);
                return new Responses(true, false);
            }
            catch (Exception)
            {
                return new Responses(false, true);
            }
        }
        public async Task<Responses> RemoveDocumentByNameAsync(string documentName)
        {
            try
            {
                CloudBlockBlob cBlob = _blobUserContainer.GetBlockBlobReference(documentName);
                var response = await cBlob.DeleteIfExistsAsync();
                if(response)
                    return new Responses(true, false);
                return new Responses(false, true);
            }
            catch (Exception)
            {
                return new Responses(false, true);
            }
        }
        public string GetContainerSasUri(int minutesToAdd = 1)
        {
            try
            {
                minutesToAdd = minutesToAdd < 0 ? 1 : minutesToAdd;

                SharedAccessBlobPolicy adHocPolicy = new SharedAccessBlobPolicy()
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(minutesToAdd),
                    Permissions = SharedAccessBlobPermissions.Read
                };

                return _blobUserContainer.GetSharedAccessSignature(adHocPolicy, null);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Responses> DeleteUserContainerAsync()
        {
            try
            {
                var response = await _blobUserContainer.DeleteIfExistsAsync();
                if (response)
                    return new Responses(true, false);
                return new Responses(false, true);
            }
            catch (Exception)
            {
                return new Responses(false, true);
            }
        }
        public string GetDocumentPath(string documentName)
        {
            try
            {
                return _blobUserContainer.GetBlockBlobReference(documentName).Uri.AbsoluteUri;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string GetConnectionString(string defaultEndpointsProtocol, string accountName, string accountKey, string EndpointSuffix)
        {
            return defaultEndpointsProtocol + accountName + accountKey + EndpointSuffix;
        }
        private CloudBlobClient BlocConnection(string connectionString)
        {
            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);
                return account.CreateCloudBlobClient();
            }
            catch (Exception)
            {
                throw new Exception("Wrong connection string");
            }
        }
        private void SelectContainer(string userId)
        {
            _blobUserContainer = _blobConnection.GetContainerReference(userId);
            _blobUserContainer.CreateIfNotExistsAsync().Wait();
        }

        public void Dispose()
        {
            _blobConnection = null;
            _blobUserContainer = null;
        }
    }
}
