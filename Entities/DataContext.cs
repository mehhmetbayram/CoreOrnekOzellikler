using Microsoft.EntityFrameworkCore;
using ProjeCoreOrnekOzellikler.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        public virtual DbSet<cdItem> CdItems { get; set; }

        public virtual DbSet<prItemPhoto> PrItemPhotos { get; set; }

        public virtual DbSet<Category> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMaps());
            modelBuilder.ApplyConfiguration(new cdItemMaps());
            modelBuilder.ApplyConfiguration(new prItemPhotoMaps());
        }

    }
}
