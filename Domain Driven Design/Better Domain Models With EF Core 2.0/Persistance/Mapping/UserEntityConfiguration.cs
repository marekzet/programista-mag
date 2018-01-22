using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Mapping
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).IsRequired();
            builder.Property("name").IsRequired().HasColumnType("varchar(120)").HasColumnName("Name");
            builder.Property("email").IsRequired().HasColumnType("varchar(80)").HasColumnName("Email");

            builder.OwnsOne(u => u.Address).Property(a => a.City).HasColumnName("City");
            builder.OwnsOne(u => u.Address).Property(a => a.Country).HasColumnName("Country");
            builder.OwnsOne(u => u.Address).Property(a => a.State).HasColumnName("State");
            builder.OwnsOne(u => u.Address).Property(a => a.Street).HasColumnName("Street");
            builder.OwnsOne(u => u.Address).Property(a => a.ZipCode).HasColumnName("ZipCode");
        }
    }
}