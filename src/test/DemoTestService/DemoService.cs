using DemoEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoTestService
{
    public class DemoService
    {
        public static IList<SampleEntity> _data;
        public IEnumerable<SampleEntity> GetDemoData()
        {
            if (_data == null)
            {
                IList<SampleEntity> data = new List<SampleEntity>();
                var random = new Random();
                for (int i = 0; i < 101; i++)
                {


                    var sampleEntity = new SampleEntity();
                    sampleEntity.Id = i;
                    sampleEntity.Name = "名称" + i;
                    sampleEntity.Title = "标题" + i;
                    sampleEntity.State = (DemoState)random.Next(0, 2);
                    data.Add(sampleEntity);

                }
                _data = data;
            }

            return _data.AsQueryable();
        }
    }
}
