using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Negotations;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation; 

public class ResolveNegotiationCommandHandler : IRequestHandler<ResolveNegotiationCommand> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;
	
	public ResolveNegotiationCommandHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(ResolveNegotiationCommand request, CancellationToken cancellationToken) {
		var negotiation = await _context.Negotiations
			.AsTracking()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

		if (negotiation is null) throw new NotFoundException(typeof(NegotiationEntity), request.Id.ToString());

		negotiation.IsAccepted = request.IsAccepted;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}