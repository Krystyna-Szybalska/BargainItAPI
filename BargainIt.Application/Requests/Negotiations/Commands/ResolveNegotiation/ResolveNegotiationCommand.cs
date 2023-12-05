using MediatR;
using Newtonsoft.Json;

namespace BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation;

public class ResolveNegotiationCommand : IRequest {
	[JsonIgnore] public required Guid Id { get; set; }
	public required bool IsAccepted { get; set; }
}