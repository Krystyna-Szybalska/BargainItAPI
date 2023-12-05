using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Application.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public UpdateProductCommandHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}

	public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
		var product = await _context.Products
			.AsTracking()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (product is null) throw new NotFoundException(typeof(ProductEntity), request.Id.ToString());

		_mapper.Map(request, product);
		await _context.SaveChangesAsync(cancellationToken);
		var productDto = _mapper.Map<ProductDto>(product);
		return productDto;
	}
}