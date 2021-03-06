using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjeCoreOrnekOzellikler.Entities;

namespace ProjeCoreOrnekOzellikler.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext dataContext)
        {
            _context = dataContext;
        }

        public List<Entities.Category> categories = new List<Entities.Category>();

        [BindProperty]
        public Entities.Category category { get; set; }

        public void OnGet()
        {
            categories = _context.Categories.ToList();
        }


        public void OnPost(Entities.Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

        }
    }
}