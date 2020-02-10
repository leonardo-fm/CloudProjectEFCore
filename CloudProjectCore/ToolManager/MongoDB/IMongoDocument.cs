using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolManager.MongoDB
{
    public interface IMongoDocument
    {
        public ObjectId _id { get; set; }
    }
}
