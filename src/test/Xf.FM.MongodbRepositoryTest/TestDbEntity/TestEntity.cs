using System;
using System.Collections.Generic;
using System.Text;
using Xf.FM.MongodbRepository;

namespace Xf.FM.MongodbRepositoryTest.TestDbEntity
{
    public class TestEntity:BaseDbEntity
    {
        public TestEntity()
        {

        }
        public string Name { get; set; }
        public int Order { get; set; }
        public byte[] Stream { get; set; }
    }
}
