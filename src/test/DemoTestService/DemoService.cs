using DemoEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoTestService
{
    public class DemoService
    {
        public static IList<SampleEntity> _data;
        public IQueryable<SampleEntity> GetDemoData()
        {
            if (_data == null)
            {
                IList<SampleEntity> data = new List<SampleEntity>();
                var random = new Random();
                for (int i = 0; i < 101; i++)
                {
                    var letter1 = random.Next(65, 91);
                    var letter2 = random.Next(65, 91);
                    var letter3 = random.Next(65, 91);
                    var letter4 = random.Next(65, 91);

                    var sampleEntity = new SampleEntity(i + 1, new string(new[] { (char)letter1, (char)letter2, (char)letter3, (char)letter4 }));
                    data.Add(sampleEntity);
                }
                _data = data;
            }
            
            return _data.AsQueryable();
        }
    }
}
