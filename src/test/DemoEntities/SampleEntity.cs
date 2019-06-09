using System;
using System.ComponentModel.DataAnnotations;

namespace DemoEntities
{
    public class SampleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DemoState State { get; set; }

        public SampleEntity() { }
        public SampleEntity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public enum DemoState
    {
        [Display(Name ="正常")]
        Normal,
        [Display(Name ="禁用")]
        Disable
    }
}
