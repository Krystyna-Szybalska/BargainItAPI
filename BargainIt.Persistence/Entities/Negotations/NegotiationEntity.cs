using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Persistence.Entities.Negotations;

public class NegotiationEntity {
	public Guid Id { get; set; }

	public required decimal ProposedPrice { get; set; }
	public bool? IsAccepted { get; set; }

	public Guid ProductId { get; set; }
	public ProductEntity? Product { get; set; }
}