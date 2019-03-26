using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xf.FM.MongodbRepository
{
    public partial class BaseDbEntity
    {
        [BsonId(IdGenerator =typeof(string))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
