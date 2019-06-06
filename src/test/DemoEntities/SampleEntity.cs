using System;

namespace DemoEntities
{
    public class SampleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public SampleEntity() { }
        public SampleEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
