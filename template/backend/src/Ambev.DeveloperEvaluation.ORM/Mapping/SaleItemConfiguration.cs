using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{   
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id)
               .HasColumnType("uuid")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(si => si.Product)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(si => si.Quantity)
               .IsRequired();

        builder.Property(si => si.UnitPrice)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(si => si.Discount)
               .IsRequired()
               .HasColumnType("decimal(18,4)")
               .HasDefaultValue(0m);

        builder.Property(si => si.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(si => si.IsCancelled)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasOne(si => si.Sale)
               .WithMany(s => s.Items)
               .HasForeignKey(si => si.SaleId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
