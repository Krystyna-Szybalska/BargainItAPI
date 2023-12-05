using MediatR;
using Newtonsoft.Json;

namespace BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation; 

public class ResolveNegotiationCommand : IRequest {
	[JsonIgnore]
	public Guid Id { get; set; }
	public bool IsAccepted { get; set; }
}