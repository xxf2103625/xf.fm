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
    /// <summary>
    /// mongodb的client由sdk自己维护线程安全，且没有dispose的必要？
    /// </summary>
    [TestClass]
    public class AddTest
    {
        public AddTest()
        {
           
        }
        protected IMgRepository<TestEntity> repository {
            get
            {
                return  new MgRepository<TestEntity>(mgConfig);
            }
        }
        protected IMgConfig mgConfig;// = new MgConfig("mongodb://192.168.0.198:27017", "Test1");
        //[TestInitialize]
        //public void Init()
        //{

        //}
        [TestMethod]
        public void TestInsertOne()
        {
            mgConfig = new MgConfig("mongodb://192.168.0.198:27017", "Test1");
            //后台进程 client链接可能无法回收 Test进程就已经结束
            var result= Parallel.For(1, 100, index =>
            {

                repository.InsertOne(new TestEntity()
                {
                    //Id = id,
                    Name = "test1",
                    Order = index,
                    Stream = new byte[10]
                });
            });
            while (!result.IsCompleted)
            {
                System.Threading.Thread.Sleep(100);
            }
            //mgConfig.Dispose();
        }
        [TestMethod]
        public async Task TestInsertManyAsync()
        {
            mgConfig = new MgConfig("mongodb://192.168.0.198:27017", "Test1");
            for (int index = 0; index < 1000; index++)
            {
                var data = new List<TestEntity>();
                for (int i = 0; i <= index + 10; i++)
                {
                    data.Add(new TestEntity() { Name = "test1", Order = i, Stream = new byte[10] });
                }
                await repository.InsertManyAsync(data);
            }
            //mgConfig.Dispose();
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
            Console.WriteLine("Client当前状态"+client.Cluster.Description.ToString());
            var database = client.GetDatabase("foo");
            var collection = database.GetCollection<Person>("person4");
            Console.WriteLine("Client当前状态" + client.Cluster.Description.ToString());
            await collection.InsertOneAsync(new Person()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                //Id="1",
                Name = "test2",
                //Order = 1,
                //Stream = new byte[10]
            });
            Console.WriteLine("Client当前状态" + client.Cluster.Description.ToString());
            var list = await collection.Find(new BsonDocument("Name", "test1"))
                .ToListAsync();
            Console.WriteLine("Client当前状态" + client.Cluster.Description.ToString());
            foreach (var document in list)
            {
                //Console.WriteLine(document["Name"]);
            }
            //client.Cluster.Dispose();
            Console.WriteLine("Client当前状态" + client.Cluster.Description.ToString());
          

        }
        //[TestCleanup]
        //public void CleanUp()
        //{
        //    mgConfig.Dispose();
        //}

        //public void Dispose()
        //{
        //    mgConfig.Dispose();
        //}
    }
}
