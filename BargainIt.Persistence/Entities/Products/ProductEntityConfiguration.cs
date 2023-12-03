using BargainIt.Persistence.Entities.Negotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BargainIt.Persistence.Entities.Products; 

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity> {
	public void Configure(EntityTypeBuilder<ProductEntity> builder) {
		builder.HasKey(x => x.Id);
		builder.HasMany<NegotiationEntity>(p => p.Negotiations)
			.WithOne(n => n.Product)
			.HasForeignKey(n => n.ProductId);
	}
}