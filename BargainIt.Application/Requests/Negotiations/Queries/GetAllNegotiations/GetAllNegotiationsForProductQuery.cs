using MediatR;

namespace BargainIt.Application.Requests.Negotiations.Queries.GetAllNegotiations; 

public class GetAllNegotiationsForProductQuery : IRequest<NegotiationDto[]> {
	public Guid ProductId { get; set; }

}