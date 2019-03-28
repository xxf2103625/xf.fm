﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xf.FM.MongodbRepository
{
    public partial class BaseDbEntity
    {
        [BsonId(IdGenerator = typeof(string))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { get; set; } = DateTime.Now;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? UTime { get; set; }
    }
}