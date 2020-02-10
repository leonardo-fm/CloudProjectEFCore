using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace ToolManager.MongoDB
{
    public class MongoDBManager
    {
        private IMongoDatabase _database { get; set; }
        private MongoClient _databaseConnection { get; set; }

        public MongoDBManager(string connectionString)
        {
            ConnectionToMongoDB(connectionString);
        }
        public MongoDBManager(string connectionString, string databaseName)
        {
            ConnectionToMongoDB(connectionString);
            Responses response = UseDatabase(databaseName);

            if (response.IsFailed)
                throw new Exception("Connection to collection problem");
        }
        public MongoDBManager(string user, string password, string host, string port, bool ssl = true)
        {
            ConnectionToMongoDB(user, password, host, port, ssl);
        }
        public MongoDBManager(string user, string password, string host, string port, string databaseName, bool ssl = true)
        {
            ConnectionToMongoDB(user, password, host, port, ssl);
            Responses response = UseDatabase(databaseName);

            if (response.IsFailed)
                throw new Exception("Connection to collection problem");
        }

        public Responses UseDatabase(string databaseName)
        {
            try
            {
                _database = _databaseConnection.GetDatabase(databaseName);
                return new Responses(true, false, false);
            }
            catch (Exception)
            {
                return new Responses(false, true, false);
            }
        }

        private void ConnectionToMongoDB(string user, string password, string host, string port, bool ssl = true)
        {
            try
            {
                string connectionString = string.Format("mongodb://{0}:{1}@{2}:{3}/?ssl={4}", user, password, host, port, ssl);

                _databaseConnection = new MongoClient(connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ConnectionToMongoDB(string connectionString)
        {
            try
            {
                _databaseConnection = new MongoClient(connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
