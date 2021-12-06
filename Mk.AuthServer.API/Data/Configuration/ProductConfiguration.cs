using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mk.AuthServer.Core.models;

namespace Mk.AuthServer.API.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
        {
            public void Configure(EntityTypeBuilder<Product> builder)
            {
                //Primary Key
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
                builder.Property(x => x.UserId).IsRequired();
            }
        }
}