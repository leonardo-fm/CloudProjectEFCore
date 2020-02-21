# [Final project for the cloud course](https://github.com/GlobalBlackout/CloudProjectEFCore)

## Requirements

- [Gmail Account](https://accounts.google.com/signup/v2/webcreateaccount?flowName=GlifWebSignIn&flowEntry=SignUp) for sending emails
- [Azure account](https://azure.microsoft.com/en-us/)
- [Azure Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)
- [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
- [Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/) with [MongoDB](https://docs.mongodb.com)

## Setup Project

- Fill **all** the fields and put all the code in cs .cs file in the directory **\CloudProjectEFCore\CloudProjectCore\CloudProjectCore\Models\Variables.cs**, copy the code under and fill all the fields, the save the file!

```C#
namespace CloudProjectCore.Models
{
    public static class Variables
    {
        public static readonly string ComputerVisionKey = @"KeyHere";
        public static readonly string ComputerVisionEndpoint = @"EndpointHere";
        
        public static readonly string BlobStorageConnectionString = @"ConnectionStringHere";
        
        public static readonly string MongoDBConnectionStringRW = @"ConnectionStringHereForReadAndWrite";
        public static readonly string MongoDBDatbaseName = @"DatabaseNameHere";
        public static readonly string MongoDBPhotosCollectionName = @"CollectionNameHere";
        
        public static readonly string EmailForSendingEmails = @"";
        public static readonly string PasswordForEmails = @"";
    }
}
```

### Copyright

The author of this software is 
[Ferrero-Merlino Leonardo](https://github.com/GlobalBlackout/)

This software is released under the [Apache License](/LICENSE), Version 2.0.
