using BargainIt.Persistence.Entities.Negotations;

namespace BargainIt.Persistence.Entities.Products; 

public class ProductEntity {
	public Guid Id { get; set; }

	public required string Name { get; set; }
	public required decimal Price { get; set; }
	public ICollection<NegotiationEntity>? Negotiations { get; set; }
}