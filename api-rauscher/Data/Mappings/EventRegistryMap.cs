using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
	public class EventRegistryMap : IEntityTypeConfiguration<EventRegistry>
	{
		public void Configure(EntityTypeBuilder<EventRegistry> builder)
		{
			builder.ToTable("EventRegistry");
			
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Id)
			.HasColumnName("Id");
			
			builder.Property(e => e.EventName)
			.HasColumnName("EventName");
			
			builder.Property(e => e.EventDescription)
			.HasColumnName("EventDescription");
			
			builder.Property(e => e.EventType)
			.HasColumnName("EventType");
			
			builder.Property(e => e.EventDate)
			.HasColumnName("EventDate");
			
			builder.Property(e => e.EventLocation)
			.HasColumnName("EventLocation");
			
			builder.Property(e => e.EventLink)
			.HasColumnName("EventLink");
			
		}
	}
}
