using Microsoft.AspNetCore.Mvc;
using PhotoAlbumWeb.AzureUtilities.Interfaces;
using PhotoAlbumWeb.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace PhotoAlbumWeb.Controllers
{
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;
        private readonly IStorage storage;
        private readonly IConfiguration configuration;
        private readonly string storageConnectionString;
        private readonly string containerName;
        
        public HomeController(ILogger<HomeController> logger,      
            IStorage storage,
            IConfiguration configuration)
        {
            _logger = logger;            
            this.storage = storage;
            this.storageConnectionString = configuration["Storage:ConnStr"];
            this.containerName = configuration["Storage:ContainerName"];  
        }

        public async Task<IActionResult> Index()
        {
            ImageModel model = new ImageModel();
            model.ImageList = await storage.GetAllImageUrls(storageConnectionString, containerName);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ImageModel model)
        {
            long size = model.Images.Sum(f => f.Length);

            foreach (var imageFile in model.Images)
            {
                if (imageFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await storage.UploadImage(storageConnectionString, containerName, stream, Path.GetExtension(imageFile.FileName));
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

                

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}