using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FormHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProjeCoreOrnekOzellikler.Entities;
using ProjeCoreOrnekOzellikler.Models;
using ProjeCoreOrnekOzellikler.Validator;

namespace ProjeCoreOrnekOzellikler.Controllers
{

    public class ProductController : Controller
    {

        private readonly DataContext _context;
        private IHostEnvironment _hostingEnvironment;




        public ProductController(DataContext context, IHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }



        string GetItemCode()
        {
            try
            {
                int numara = int.Parse(_context.CdItems.OrderByDescending(x => x.Id).First().ItemCode.Substring(2));
                numara++;

                return "P-" + numara.ToString();
            }
            catch
            {
                return "P-10000001";
            }
        }

        ProductModel GetProductModel()
        {
            var model = new ProductModel
            {
                Product = new cdItem()
                {
                    CreateTime = TimeSpan.Parse(DateTime.Now.ToShortTimeString()),
                    CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString())
                },
                ProductList = _context.CdItems.ToList(),
                CategoryList = _context.Categories.ToList(),
                prItemPhotos = _context.PrItemPhotos.ToList(),
                JsonMessage=new JsonMessage
                {
                    IsSuccess=true,
                    UserMessage=""
                }

            };

            return model;
        }

        public IActionResult Index()
        {


            return View(GetProductModel());
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductModel model, IEnumerable<IFormFile> formFiles)
        {
            var validator = new ProductValidator();
            var result = validator.Validate(model.Product);

            if (result.Errors.Count>0)
            {

                var errorModel = GetProductModel();
                errorModel.Product = model.Product;
                errorModel.JsonMessage.IsSuccess = false;
               
                foreach (var item in result.Errors)
                {
                   
                    ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
                    errorModel.JsonMessage.UserMessage += " " + item.ErrorMessage;
                }
               

                return View("Index",errorModel);
            }
            
                string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string directoryImage = Path.Combine(webRootPath, "Images");


                if (!Directory.Exists(directoryImage))
                {
                    System.IO.Directory.CreateDirectory(directoryImage);
                }

                model.Product.ItemCode = GetItemCode();
                _context.CdItems.Add(model.Product);
                _context.SaveChanges();


                if (formFiles.Count() > 0)
                {
                    foreach (var item in formFiles)
                    {
                        var prItemPhoto = new prItemPhoto();


                        string imagePath = Path.Combine(directoryImage, item.FileName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        prItemPhoto.cdItemId = model.Product.Id;
                        prItemPhoto.ImagePath = "/Images/" + item.FileName;
                        _context.PrItemPhotos.Add(prItemPhoto);
                        _context.SaveChanges();
                    }
                }



                return RedirectToAction("Index");
            
        }



        public IActionResult RemoveProduct(int id)
        {
            _context.CdItems.Remove(_context.CdItems.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
            return ViewComponent("ProductList");
        }









    }
}