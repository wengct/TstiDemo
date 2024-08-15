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

        public HomeController(ILogger<HomeController> logger,
                              [FromKeyedServices("ftp")] IFile ftpFile,
                              [FromKeyedServices("io")] IFile ioFile)
        {
            _logger = logger;
            _ftpFile = ftpFile;
            _ioFile = ioFile;
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
