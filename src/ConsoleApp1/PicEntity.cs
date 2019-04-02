using System;
using System.Collections.Generic;
using System.Text;
using Xf.FM.MongodbRepository;

namespace ConsoleApp1
{
    public class PicEntity : BaseMgDbEntity
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string PicType { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
    }
}
