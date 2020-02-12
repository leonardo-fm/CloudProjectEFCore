# [Final project for the cloud course](https://github.com/GlobalBlackout/CloudProjectEFCore)

## Requirements

- [Gmail Account](https://accounts.google.com/signup/v2/webcreateaccount?flowName=GlifWebSignIn&flowEntry=SignUp) for sending emails
- [Azure account](https://azure.microsoft.com/en-us/)
- [Azure Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)
- [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
- [Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/) with [MongoDB](https://docs.mongodb.com)

## Setup Project

- Fill **all** the fields and put all the code in a .cs file in the **\CloudProjectEFCore\CloudProjectCore\CloudProjectCore\Models\Variables.cs**, to create the file **Variables.cs** just open a text editor (notepad, Visual studio Code, etc...), copy the code under and fill all the fields, the save the file with the name **Variables.cs** be care it doesn't have any other extension beyond the **.cs**!

```C#
namespace CloudProjectCore.Models
{
    public static class Variables
    {
        public static readonly string ComputerVisionKey = @"KeyHere";
        public static readonly string ComputerVisionEndpoint = @"EndpointHere";
        
        public static readonly string BlobStorageConnectionString = @"ConnectionStringHere";
        
        public static readonly string MongoDBConnectionStringRW = @"ConnectionStringHere";
        public static readonly string MongoDBConnectionStringR = @"ConnectionStringHere";
        public static readonly string MongoDBDatbaseName = @"DatabaseNameHere";
        public static readonly string MongoDBPhotosCollectionName = @"CollectionNameHere";
    }
}
```

### Copyright

The author of this software is 
[Ferrero-Merlino Leonardo](https://github.com/GlobalBlackout/)

This software is released under the [Apache License](/LICENSE), Version 2.0.
