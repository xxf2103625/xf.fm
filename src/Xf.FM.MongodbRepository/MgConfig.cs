using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xf.FM.MongodbRepository
{
    public class MgConfig : IMgConfig
    {
        private MongoClientSettings _mongoClientSettings;
        private string _dbName;
        public MgConfig(string connStr, string dbName)
        {
            _mongoClientSettings = MongoClientSettings.FromConnectionString(connStr);
            _dbName = dbName;
        }
        public MgConfig(MongoClientSettings mongoClientSettings)
        {
            _mongoClientSettings = mongoClientSettings;
        }
        public MongoClientSettings GetMgConfig()
        {
            return _mongoClientSettings;
        }
        public string GetDbName()
        {
            return _dbName;
        }
    }
}
