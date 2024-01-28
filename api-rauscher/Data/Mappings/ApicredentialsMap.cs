using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
	public class ApiCredentialsMap : IEntityTypeConfiguration<ApiCredentials>
	{
		public void Configure(EntityTypeBuilder<ApiCredentials> builder)
		{
			builder.ToTable("ApiCredentials");
			
			builder.HasKey(e => e.Apikey);
			builder.Property(e => e.Apikey)
			.HasColumnName("ApiKey");
			
			builder.Property(e => e.Apisecrethash)
			.HasColumnName("ApiSecretHash");
			
			builder.Property(e => e.Createdat)
			.HasColumnName("CreatedAt");
			
			builder.Property(e => e.Lastusedat)
			.HasColumnName("LastUsedAt");
			
			builder.Property(e => e.Isactive)
			.HasColumnName("IsActive");
			
		}
	}
}
