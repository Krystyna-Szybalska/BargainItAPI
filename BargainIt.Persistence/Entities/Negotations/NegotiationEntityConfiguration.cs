using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BargainIt.Persistence.Entities.Negotations; 

public class NegotiationEntityConfiguration : IEntityTypeConfiguration<NegotiationEntity> {
	public void Configure(EntityTypeBuilder<NegotiationEntity> builder) {
		builder.HasKey(x => x.Id);
	}
}