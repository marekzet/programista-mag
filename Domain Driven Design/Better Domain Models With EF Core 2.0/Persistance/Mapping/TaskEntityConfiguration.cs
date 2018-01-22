using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Mapping
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired();
            builder.Property("name").IsRequired().HasColumnType("varchar(160)").HasColumnName("Name");
            builder.Property("description").HasColumnType("varchar(max)").HasColumnName("Description");
            builder.Property<int>("statusId").IsRequired().HasColumnName("StatusId");

            builder.HasOne<TaskStatus>()
                .WithMany()
                .HasForeignKey("statusId");
        }
    }
}