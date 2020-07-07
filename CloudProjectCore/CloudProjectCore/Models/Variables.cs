namespace CloudProjectCore.Models
{
    public static class Variables
    {
        public static readonly string ComputerVisionKey = @"cb1109b4c9f54f36a4d41c62b760e555";
        public static readonly string ComputerVisionEndpoint = @"https://northeurope.api.cognitive.microsoft.com/";
        
        public static readonly string BlobStorageConnectionString = @"DefaultEndpointsProtocol=https;AccountName=progettocloudstorage;AccountKey=Z00ylY9K3AweU3uK4asR+0dVz29dqmqlJjLNa3LnH9eiFClkXGnAaW6OkfZ/Q6brAEtPTpSuSmIX07Le4rrr3g==;EndpointSuffix=core.windows.net";
        
        public static readonly string MongoDBConnectionStringRW = @"mongodb://dbprogettocloud:klJYo60zLNvXi3UfSBgCAbRvhxpJDbuKNmAoma01LJhr2qi6OHiV99cYEx6osHGbTvRRT9q84aNOPltQr4aPmA==@dbprogettocloud.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
        public static readonly string MongoDBDatbaseName = @"cloudProjectDB";
        public static readonly string MongoDBPhotosCollectionName = @"photos";

        public static readonly string EmailForSendingEmails = @"progettocloud2019its@gmail.com";
        public static readonly string PasswordForEmails = @"Progetto2019!";
    }
}
