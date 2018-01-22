using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Mapping
{
    public class BacklogItemStatusEntityConfiguration : IEntityTypeConfiguration<BacklogItemStatus>
    {
        public void Configure(EntityTypeBuilder<BacklogItemStatus> builder)
        {
            builder.ToTable("BacklogItemStatus");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(s => s.Name)
                .HasColumnType("varchar(60)")
                .IsRequired();
        }
    }
}