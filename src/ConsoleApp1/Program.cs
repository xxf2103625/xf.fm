using MongoDB.Driver;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xf.FM.MongodbRepository;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task(async () => await DownData());
            task.Start();
            Console.Read();
        }
        static void GetData()
        {
            var client = new MongoClient(MongoClientSettings.FromConnectionString("mongodb://192.168.0.198:27017"));
            var database = client.GetDatabase("picdown");
            var db = database.GetCollection<PicEntity>(nameof(PicEntity));


            IWorkbook workbook = null;
            string fileName = "tableExport2.xls";

            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook(fs);
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook(fs);
            var sheet = workbook.GetSheetAt(0);
            int rowL = sheet.PhysicalNumberOfRows;
            for (int i = 1; i < rowL; i++)
            {
                IRow row = sheet.GetRow(i);
                PicEntity entity = new PicEntity();
                entity.Url = row.GetCell(4).StringCellValue;
                entity.Height = (int)row.GetCell(5).NumericCellValue;
                entity.Width = (int)row.GetCell(6).NumericCellValue;
                entity.PicType = row.GetCell(7).StringCellValue;
                entity.Type = row.GetCell(8).StringCellValue;
                entity.Remark = row.GetCell(13).StringCellValue;
                db.InsertOne(entity);
                //Console.WriteLine(row.GetCell(4));
            }
            client.Cluster.Dispose();
        }

        static string basePath = "pic/";
        static async Task DownFileAsync(string dic, string url)
        {
            string fileName = url.Split("/").ToList().Last();
            dic = basePath + dic + "/";
            //if(Directory.Exists())
            if (!Directory.Exists(dic))
            {
                Directory.CreateDirectory(dic);
            }
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var bts = await httpClient.GetByteArrayAsync(url);
                    using (FileStream file = new FileStream(dic + fileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        file.Write(bts);
                        file.Flush();
                        file.Close();
                    }
                    Console.WriteLine("下载完成："+dic + fileName);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
        static async Task DownData()
        {
            var mgConfig = new MgConfig("mongodb://192.168.0.198:27017", "picdown");
            var db = new MgRepository<PicEntity>(mgConfig);
            //var first = db.Query.FirstOrDefault();
            //await DownFileAsync(first.Type, first.Url);

            foreach (var item in db.Query.ToList())
            {
                await DownFileAsync(item.Type, item.Url);
            }





            mgConfig.Dispose();
        }
    }
}
