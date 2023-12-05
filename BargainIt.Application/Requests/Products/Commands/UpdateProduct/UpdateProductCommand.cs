using MediatR;
using Newtonsoft.Json;

namespace BargainIt.Application.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ProductDto> {
	[JsonIgnore] public required Guid Id { get; set; }
	public required string Name { get; set; }
	public decimal Price { get; set; }
}