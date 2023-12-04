using BargainIt.Application.Requests.Negotiations;
using BargainIt.Application.Requests.Negotiations.Commands.CreateCommand;
using BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation;
using BargainIt.Application.Requests.Negotiations.Queries.GetAllNegotiations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BargainIt.Api.Controllers; 

[ApiController]
[Produces("application/json")]
[Route("api/negotiations")] 
public class NegotiationsController : ControllerBase{
	private readonly IMediator _mediator;

	public NegotiationsController(IMediator mediator) {
		_mediator = mediator;
	}
	
	[HttpPost]
	[ProducesResponseType(typeof(NegotiationDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<NegotiationDto>> Create(CreateNegotiationCommand request,
		CancellationToken cancellationToken) {
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}
	
	[HttpGet]
	[ProducesResponseType(typeof(NegotiationDto[]), StatusCodes.Status200OK)]
	public async Task<ActionResult<NegotiationDto[]>> GetAll([FromQuery] GetAllNegotiationsForProductQuery request,
		CancellationToken cancellationToken) {
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}

	[HttpPut("{id:guid}/resolve")]
	[ProducesResponseType( StatusCodes.Status200OK)] 
	public async Task<Unit> ResolveNegotiation(Guid id, ResolveNegotiationCommand request,
		CancellationToken cancellationToken) {
		request.Id = id;
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}
}