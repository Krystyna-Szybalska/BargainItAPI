using MediatR;

namespace BargainIt.Application.Requests.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ProductDto> {
	public required string Name { get; set; }
	public required decimal Price { get; set; }
}