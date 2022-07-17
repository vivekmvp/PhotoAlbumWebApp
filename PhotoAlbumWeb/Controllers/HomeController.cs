using Microsoft.AspNetCore.Mvc;
using PhotoAlbumWeb.Models;
using System.Diagnostics;

namespace PhotoAlbumWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ImageDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, 
            IWebHostEnvironment hostEnvironment,
            ImageDbContext context)
        {
            _logger = logger;
            this._hostEnvironment = hostEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(List<IFormFile> Images)
        {
            long size = Images.Sum(f => f.Length);

            foreach (var imageFile in Images)
            {
                if (imageFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
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