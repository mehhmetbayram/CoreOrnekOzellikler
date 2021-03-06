using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Entities.Mapping
{
    public class prItemPhotoMaps : IEntityTypeConfiguration<prItemPhoto>
    {
        public void Configure(EntityTypeBuilder<prItemPhoto> builder)
        {
            builder.ToTable("prItemPhotos");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.cdItemId)
                .HasColumnName("cdItemId")
                .IsRequired();

            builder.Property(t => t.ImagePath)
                .HasColumnName("Path")
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(t => t.CdItem)
                .WithMany(t => t.PrItemPhotos)
                .HasForeignKey(t => t.cdItemId);
        }
    }
}
