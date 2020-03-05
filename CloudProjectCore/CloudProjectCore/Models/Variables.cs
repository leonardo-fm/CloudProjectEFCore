namespace CloudProjectCore.Models
{
    public static class Variables
    {
        public static readonly string ComputerVisionKey = @"KeyHere";
        public static readonly string ComputerVisionEndpoint = @"EndpointHere";
        
        public static readonly string BlobStorageConnectionString = @"ConnectionStringHere";
        
        public static readonly string MongoDBConnectionStringRW = @"ConnectionStringForReadAndWriteHere";
        public static readonly string MongoDBDatbaseName = @"DatabaseNameHere";
        public static readonly string MongoDBPhotosCollectionName = @"CollectionNameHere";
        
        public static readonly string EmailForSendingEmails = @"EmailForSendingEmailsHere";
        public static readonly string PasswordForEmails = @"PasswordForTheEmailAccountHere";
    }
}
