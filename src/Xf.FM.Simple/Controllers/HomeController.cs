using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DemoTestService;
using Microsoft.AspNetCore.Mvc;
using Xf.FM.DataTables;
using Xf.FM.Simple.Models;

namespace Xf.FM.Simple.Controllers
{
    public class HomeController : Controller
    {
        DemoService _demoService;
        public HomeController(DemoService demoService)
        {
            _demoService = demoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BaseTable()
        {
            IEnumerable<DemoEntities.SampleEntity> data = _demoService.GetDemoData();
            return View(data);
        }
        public IActionResult Tables()
        {
            return View();
        }
        public IActionResult TablesAjax(IDTRequest request)
        {
            var data= _demoService.GetDemoData();
            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = String.IsNullOrWhiteSpace(request.Search.Value)
                ? data
                : data.Where(_item => _item.Name.Contains(request.Search.Value));

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DTResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DTJsonResult(response, true);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
