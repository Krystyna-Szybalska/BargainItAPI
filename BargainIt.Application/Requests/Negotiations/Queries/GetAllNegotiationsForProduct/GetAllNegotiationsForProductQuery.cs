using MediatR;

namespace BargainIt.Application.Requests.Negotiations.Queries.GetAllNegotiationsForProduct;

public class GetAllNegotiationsForProductQuery : IRequest<NegotiationDto[]> {
	public Guid ProductId { get; set; }
}