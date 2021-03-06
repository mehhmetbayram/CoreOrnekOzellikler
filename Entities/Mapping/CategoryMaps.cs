using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities.Mapping
{
    public class CategoryMaps : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.ParentId)
                .HasColumnName("ParentCategory")
                .IsRequired(false)
                .HasDefaultValue(0);



            builder.Property(t => t.CategoryName)
                .HasColumnName("CategoryName")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
