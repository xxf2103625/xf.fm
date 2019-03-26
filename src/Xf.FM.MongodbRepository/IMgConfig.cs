using MongoDB.Driver;
using System;

namespace Xf.FM.MongodbRepository
{
    public interface IMgConfig
    {
        MongoClientSettings GetMgConfig();
        string GetDbName();
    }
}
