using MediatR;

namespace BargainIt.Application.Requests.Negotiations.Commands.CreateCommand; 

public class CreateNegotiationCommand : IRequest<NegotiationDto>{
	public required decimal ProposedPrice { get; set; }
	public required Guid ProductId { get; set; }

}