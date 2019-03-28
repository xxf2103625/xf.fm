using MongoDB.Driver;
using System;

namespace Xf.FM.MongodbRepository
{
    /// <summary>
    /// 文档 ref="http://mongodb.github.io/mongo-csharp-driver/2.7/getting_started/"
    /// mongodbC#驱动源码 ref="https://github.com/mongodb/mongo-csharp-driver"
    /// </summary>
    public interface IMgConfig:IDisposable
    {
        MongoClientSettings GetMgConfig();
        string GetDbName();
        IMongoClient Client { get; }
    }
}
