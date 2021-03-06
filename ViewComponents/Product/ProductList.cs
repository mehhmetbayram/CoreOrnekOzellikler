using Microsoft.AspNetCore.Mvc;
using ProjeCoreOrnekOzellikler.Entities;
using ProjeCoreOrnekOzellikler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.ViewComponents.Product
{
    public class ProductList : ViewComponent
    {

        private readonly DataContext _context;

        public ProductList(DataContext context)
        {
            _context = context;
        }


        public IViewComponentResult Invoke()
        {
            var model = new ProductModel
            {
                CategoryList = _context.Categories.ToList(),
                ProductList = _context.CdItems.ToList(),
                prItemPhotos = _context.PrItemPhotos.ToList()
            };
            return View(model);
        }


    }
}
