using MediatR;
using Newtonsoft.Json;

namespace BargainIt.Application.Requests.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest {
	[JsonIgnore] public Guid Id { get; set; }
}