
using ProjeCoreOrnekOzellikler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Models
{
    public class ProductModel
    {
        public IEnumerable<cdItem> ProductList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<prItemPhoto> prItemPhotos { get; set; }
        public JsonMessage JsonMessage  { get; set; }
        public cdItem Product { get; set; }


    }
}
