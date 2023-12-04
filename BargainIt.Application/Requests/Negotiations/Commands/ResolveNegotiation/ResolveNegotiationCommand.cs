using MediatR;

namespace BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation; 

public class ResolveNegotiationCommand : IRequest {
	public Guid Id { get; set; }
	public bool IsAccepted { get; set; }
}