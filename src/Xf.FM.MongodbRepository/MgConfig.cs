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
        private IMongoClient _client;
        private static object CLIENT_LOCK = new object();
        public IMongoClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (CLIENT_LOCK)
                    {
                        if (_client == null)
                        {
                            _client = new MongoClient(GetMgConfig());
                            Console.WriteLine("创建新MongoClient:" + _client.Cluster.ClusterId);
                        }
                        else
                            return _client;
                    }
                }
                return _client;
            }
        }

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

        public void Dispose()
        {
            if (_client != null)
            {
                Console.WriteLine("销毁_client:" + _client.Cluster.ClusterId);
                _client.Cluster.Dispose();
            }
        }
    }
}
