using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Requests.Products;
using BargainIt.Application.Requests.Products.Queries.GetAllProducts;
using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Products;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.Application.Requests.Negotiations.Queries.GetAllNegotiations; 

public class GetAllNegotiationsForProductQueryHandler : IRequestHandler<GetAllNegotiationsForProductQuery, NegotiationDto[]> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetAllNegotiationsForProductQueryHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}
	
	
	public async Task<NegotiationDto[]> Handle(GetAllNegotiationsForProductQuery request, CancellationToken cancellationToken) {
		var product = await _context.Products
			.AsNoTracking()
			.Include(productEntity => productEntity.Negotiations)
			.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken: cancellationToken);

		if (product is null) throw new NotFoundException(typeof(ProductEntity), request.ProductId.ToString());

		var negotiations = product.Negotiations;   
		
		var negotiationDtos = _mapper.Map<NegotiationDto[]>(negotiations);
		return negotiationDtos;
	}
}