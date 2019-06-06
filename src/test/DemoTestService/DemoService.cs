using DemoEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoTestService
{
    public class DemoService
    {
        public IQueryable<SampleEntity> GetDemoData()
        {
            IList<SampleEntity> data = new List<SampleEntity>();
            var random = new Random();
            for (int i = 0; i < 53; i++)
            {
                var letter1 = random.Next(65, 91);
                var letter2 = random.Next(65, 91);
                var letter3 = random.Next(65, 91);
                var letter4 = random.Next(65, 91);

                var sampleEntity = new SampleEntity(i + 1, new string(new[] { (char)letter1, (char)letter2, (char)letter3, (char)letter4 }));
                data.Add(sampleEntity);
            }
            return data.AsQueryable();
        }
    }
}
