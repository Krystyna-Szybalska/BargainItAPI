using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Behaviour.Exceptions.ErrorCode;
using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Negotations;
using BargainIt.Persistence.Entities.Products;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.Application.Requests.Negotiations.Commands.CreateNegotiation;

public class CreateNegotiationCommandHandler : IRequestHandler<CreateNegotiationCommand, NegotiationDto> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public CreateNegotiationCommandHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}

	public async Task<NegotiationDto> Handle(CreateNegotiationCommand request, CancellationToken cancellationToken) {
		var product = await _context.Products
			.AsNoTracking()
			.Include(productEntity => productEntity.Negotiations)
			.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

		if (product is null) throw new NotFoundException(typeof(ProductEntity), request.ProductId.ToString());

		if (request.ProposedPrice > 2 * product.Price)
			throw new VerificationException(ErrorCodes.Negotiations.PriceTooHighError,
				"Proposed price cannot be two times greater than product's price");

		if (product.Negotiations.Any(n => n.IsAccepted == true))
			throw new VerificationException(ErrorCodes.Negotiations.NegotiationAlreadyAccepted,
				"Previous client proposition already got accepted");

		if (product.Negotiations.Count >= 3)
			throw new VerificationException(ErrorCodes.Negotiations.TooManyNegotiationAttempts,
				"There might be maximum three negotiation attempts");

		var negotiation = _mapper.Map<NegotiationEntity>(request);
		negotiation.Id = Guid.NewGuid();

		_context.Negotiations.Add(negotiation);
		await _context.SaveChangesAsync(cancellationToken);

		var negotiationDto = _mapper.Map<NegotiationDto>(negotiation);
		return negotiationDto;
	}
}