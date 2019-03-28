using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xf.FM.MongodbRepository;
using Xf.FM.MongodbRepositoryTest.TestDbEntity;

namespace Xf.FM.MongodbRepositoryTest
{
    [TestClass]
    public class AddTest
    {

        protected IMgRepository<TestEntity> repository;
        IMgConfig mgConfig;
        [TestInitialize]
        public void Init()
        {
            mgConfig = new MgConfig("mongodb://192.168.0.198:27017", "Test1");
            repository = new MgRepository<TestEntity>(mgConfig);
        }
        [TestMethod]
        public void TestInsertOne()
        {
            //后台进程 client链接可能无法回收
            Parallel.For(1, 10000, index =>
            {

                repository.InsertOne(new TestEntity()
                {
                    //Id = id,
                    Name = "test1",
                    Order = index,
                    Stream = new byte[10]
                });
            });

        }
        [TestMethod]
        public async Task TestInsertManyAsync()
        {
            for (int index = 0; index < 1000; index++)
            {
                var data = new List<TestEntity>();
                for (int i = 0; i <= index + 10; i++)
                {
                    data.Add(new TestEntity() { Name = "test1", Order = i, Stream = new byte[10] });
                }
                await repository.InsertManyAsync(data);
            }
        }
        public class Person
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public string Name { get; set; }
        }
        [TestMethod]
        public async Task TestMethod2Async()
        {
            //var client = new MongoClient("mongodb://192.168.0.198:27017");
            var client = new MongoClient(MongoClientSettings.FromConnectionString("mongodb://192.168.0.198:27017"));
            var database = client.GetDatabase("foo");
            var collection = database.GetCollection<Person>("person4");

            await collection.InsertOneAsync(new Person()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                //Id="1",
                Name = "test2",
                //Order = 1,
                //Stream = new byte[10]
            });

            var list = await collection.Find(new BsonDocument("Name", "test1"))
                .ToListAsync();

            foreach (var document in list)
            {
                //Console.WriteLine(document["Name"]);
            }
        }
        [TestCleanup]
        public void CleanUp()
        {
            //mgConfig.Dispose();
        }
    }
}
