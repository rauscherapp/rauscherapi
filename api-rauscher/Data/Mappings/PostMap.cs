using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
	public class PostMap : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.ToTable("Post");
			
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id)
			.HasColumnName("ID");
			
			builder.Property(e => e.Title)
			.HasColumnName("TITLE");
			
			builder.Property(e => e.CreatedDate)
			.HasColumnName("CREATEDATE");
			
			builder.Property(e => e.Content)
			.HasColumnName("CONTENT");
			
			builder.Property(e => e.Author)
			.HasColumnName("AUTHOR");
			
			builder.Property(e => e.Visible)
			.HasColumnName("VISIBLE");
			
			builder.Property(e => e.PublishedAt)
			.HasColumnName("PUBLISHEDAT");
			
			builder.Property(e => e.FolderId)
			.HasColumnName("FolderId");			
			builder.Property(e => e.Language)
			.HasColumnName("Language");
			
		}
	}
}
