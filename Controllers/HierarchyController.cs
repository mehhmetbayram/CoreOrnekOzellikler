using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeCoreOrnekOzellikler.Entities;

namespace ProjeCoreOrnekOzellikler.Controllers
{
 
    public class HierarchyController : Controller
    {
        private DataContext _context;

        public HierarchyController(DataContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            var data = _context.Categories.ToList();
            return View(data);
        }


    }
}