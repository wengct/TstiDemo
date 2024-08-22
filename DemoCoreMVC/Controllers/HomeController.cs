using DemoCoreMVC.DAL;
using DemoCoreMVC.Files;
using DemoCoreMVC.Interfaces;
using DemoCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;

namespace DemoCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFile _ftpFile;
        private readonly IFile _ioFile;
        private readonly AdventureWorksLT2022Context _db;

        // «Øºc¦¡ª`¤J
        public HomeController(ILogger<HomeController> logger,
                               [FromKeyedServices(key: "ftp")] IFile ftpFile,
                              [FromKeyedServices("io")] IFile ioFile,
                              AdventureWorksLT2022Context db)
        {
            _logger = logger;
            _ftpFile = ftpFile;
            _ioFile = ioFile;
            _db = db; // Scoped
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Hello, Index page visited");

            var fileByte = _ioFile.ReadAllBytes(@"D:\Git\Demo(Core)\DemoConsole\Test.txt");
            ViewBag.FileContent = System.Text.Encoding.UTF8.GetString(fileByte);
            return View();
        }

        public IActionResult Privacy()
        {
            var product = _db.Product.FirstOrDefault();

            if (product != null)
            {
                ViewBag.ProductName = product.Name;
            }
            else
            {
                ViewBag.ProductName = "Not Found";
            }

            //ViewBag.ProductName = product != null ? product.Name : "Not Found";

            //ViewBag.ProductName = product?.Name ?? "Not Found";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
