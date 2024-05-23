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
			
			builder.HasKey(e => e.EventRegistryId);
			builder.Property(e => e.EventRegistryId)
			.HasColumnName("Id");
			
			builder.Property(e => e.Eventname)
			.HasColumnName("EventName");
			
			builder.Property(e => e.Eventdescription)
			.HasColumnName("EventDescription");
			
			builder.Property(e => e.Eventtype)
			.HasColumnName("EventType");
			
			builder.Property(e => e.Eventdate)
			.HasColumnName("EventDate");
			
			builder.Property(e => e.Eventlocation)
			.HasColumnName("EventLocation");
			
			builder.Property(e => e.Eventlink)
			.HasColumnName("EventLink");
			
		}
	}
}
