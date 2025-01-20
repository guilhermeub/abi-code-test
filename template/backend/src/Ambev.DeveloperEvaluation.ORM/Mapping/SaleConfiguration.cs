using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
               .HasColumnType("uuid")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(s => s.TotalSaleAmount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(s => s.SaleDate)
               .IsRequired();

        builder.Property(s => s.Branch)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(s => s.IsCancelled)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasOne(s => s.Customer) 
               .WithMany()
               .HasForeignKey(s => s.Id)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Items)
               .WithOne(i => i.Sale)
               .HasForeignKey(i => i.SaleId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
