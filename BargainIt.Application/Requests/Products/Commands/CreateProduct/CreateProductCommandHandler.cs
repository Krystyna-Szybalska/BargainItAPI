using MapsterMapper;
using MediatR;
using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Application.Requests.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public CreateProductCommandHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}

	public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
		var product = _mapper.Map<ProductEntity>(request);
		product.Id = Guid.NewGuid();
		_context.Products.Add(product);
		await _context.SaveChangesAsync(cancellationToken);
		var productDto = _mapper.Map<ProductDto>(product);
		return productDto;
	}
}