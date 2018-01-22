using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Mapping
{
    public class BacklogItemEntityConfiguration : IEntityTypeConfiguration<BacklogItem>
    {
        public void Configure(EntityTypeBuilder<BacklogItem> builder)
        {
            builder.ToTable("BacklogItem");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).IsRequired();
            builder.Property("name").IsRequired().HasColumnType("varchar(160)").HasColumnName("Name");
            builder.Property("description").HasColumnType("varchar(max)").HasColumnName("Description");
            builder.Property("createdOn").IsRequired().HasColumnName("CreatedOn");
            builder.Property("closedOn").HasColumnName("ClosedOn");
            builder.Property<int>("statusId").HasColumnName("StatusId");
            builder.Property<int?>("ownerId").HasColumnName("OwnerId");

            var navigation = builder.Metadata.FindNavigation(nameof(BacklogItem.Tasks));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey("ownerId");

            builder.HasOne<BacklogItemStatus>()
                 .WithMany()
                 .HasForeignKey("statusId");
        }
    }
}