# [Final project for the cloud course](https://github.com/GlobalBlackout/CloudProjectEFCore)

This software is a cloud-based gallery for photos, is written in ASP.NET Core 3.1.

## Requirements

- [Gmail Account](https://accounts.google.com/signup/v2/webcreateaccount?flowName=GlifWebSignIn&flowEntry=SignUp) for sending emails
- [Azure account](https://azure.microsoft.com/en-us/)
- [Azure Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)
- [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
- [Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/) with [MongoDB](https://docs.mongodb.com)

## Setup Project

- Fill **all** the fields of the file in the directory **CloudProjectEFCore\CloudProjectCore\CloudProjectCore\Models\Variables.cs**.
- [Create an instance](https://docs.microsoft.com/en-us/azure/cosmos-db/create-mongodb-dotnet) of MongoDB in the Cosmos DB, and then [add a collection](https://docs.microsoft.com/en-us/cli/azure/cosmosdb/mongodb/collection?view=azure-cli-latest) for the photos.

### Copyright

The author of this software is 
[Ferrero-Merlino Leonardo](https://github.com/leonardo-fm/)

This software is released under the [Apache License](/LICENSE), Version 2.0.
