using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjeCoreOrnekOzellikler.Models;

namespace ProjeCoreOrnekOzellikler.Controllers
{
   
    public class WebPImageController : Controller
    {
        private IHostEnvironment _hostingEnvironment;

        public WebPImageController(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View(new ImageModel());
        }

        [HttpPost]
        public IActionResult AddImage(IFormFile formFile)
        {
            string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string directoryPath = Path.Combine(webRootPath, "UploadImages");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }


            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            string fullPath = Path.Combine(directoryPath, fileName);
            string webPFileName = Path.Combine(directoryPath, Path.GetFileNameWithoutExtension(formFile.FileName) + ".webp");

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            using (var webPFileStream = new FileStream(webPFileName, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {

                    imageFactory.Load(formFile.OpenReadStream())
                                .Format(new WebPFormat())
                                //.Quality(100)
                                //.Contrast(1500)
                                //.BackgroundColor(Color.Yellow)
                                .Save(webPFileStream);
                }
            }

            string uriP = Path.GetFileNameWithoutExtension(formFile.FileName) + ".webp";
            var model = new ImageModel
            {
                ImagePath = "/UploadImages/" + uriP
            };
            return View("Index", model);
        }



    }
}