using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities.Mapping
{
    public class cdItemMaps : IEntityTypeConfiguration<cdItem>
    {
        public void Configure(EntityTypeBuilder<cdItem> builder)
        {
            builder.ToTable("cdItems");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.ItemCode)
                .HasColumnName("ItemCode")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.ItemName)
                .HasColumnName("ItemDescription")
                .HasMaxLength(75)
                .IsRequired();


            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasColumnType("ntext");

            builder.Property(t => t.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("date")
                .HasDefaultValueSql("getdate()");

            builder.Property(t => t.CreateTime)
                .HasColumnName("CreateTime")
                .HasColumnType("Time");
                //.HasDefaultValue(DateTime.Now.ToShortTimeString());

            builder.Property(t => t.IsActive)
                .HasColumnName("IsActive")
                .HasDefaultValue(true);

            builder.Property(t => t.Qty)
                .HasColumnName("Qty")
                .HasDefaultValue(0);

            builder.Property(t => t.Price)
                .HasColumnName("Price")
                .HasColumnType("money")
                .HasDefaultValue(0);

            builder.Property(t => t.CategoryId)
                .HasColumnName("CategoryId")
                .IsRequired()
                .HasDefaultValue(-1);

            builder.HasOne(t => t.Category)
                .WithMany(t => t.CdItems)
                .HasForeignKey(t => t.CategoryId);




        }
    }
}
