using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeCoreOrnekOzellikler.Entities;
using ProjeCoreOrnekOzellikler.Models;

namespace ProjeCoreOrnekOzellikler.Controllers
{

    public class SayfalamaController : Controller
    {
        DataContext _context;
        public SayfalamaController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public IActionResult Index(int currentPage = 1, int category = 0)
        {


            int pageSize = 100;

            var products = _context.CdItems.Where(x => x.CategoryId == category || category == 0).ToList();

            var model = new ItemModel
            {
                CurrentCategory = category,
                CurrentPage = currentPage,
                PageSize = pageSize,
                CdItems = products.Skip(pageSize * (currentPage - 1)).Take(pageSize).OrderBy(x => x.Id).ToList(),
                Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList(),
                PageCount = (int)Math.Ceiling((decimal)products.Count / pageSize),
                TotalCountProduct = products.Count

            };
            return View(model);
        }
    }
}