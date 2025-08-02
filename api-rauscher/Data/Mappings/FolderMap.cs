using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
	public class FolderMap : IEntityTypeConfiguration<Folder>
	{
		public void Configure(EntityTypeBuilder<Folder> builder)
		{
			builder.ToTable("Folder");
			
			builder.HasKey(e => e.ID);
			builder.Property(e => e.ID)
            .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("ID");
			
			builder.Property(e => e.TITLE)
			.HasColumnName("TITLE");
			
			builder.Property(e => e.SLUG)
			.HasColumnName("SLUG");
			
			builder.Property(e => e.ICON)
			.HasColumnName("ICON");
			
		}
	}
}
