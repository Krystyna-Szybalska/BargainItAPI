using MediatR;
using Microsoft.AspNetCore.Mvc;
using BargainIt.Application.Requests.Products;
using BargainIt.Application.Requests.Products.Commands.CreateProduct;
using BargainIt.Application.Requests.Products.Commands.DeleteProduct;
using BargainIt.Application.Requests.Products.Commands.UpdateProduct;
using BargainIt.Application.Requests.Products.Queries.GetAllProducts;

namespace BargainIt.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/products")]
public class ProductsController : ControllerBase {
	private readonly IMediator _mediator;

	public ProductsController(IMediator mediator) {
		_mediator = mediator;
	}

	[HttpGet]
	[ProducesResponseType(typeof(ProductDto[]), StatusCodes.Status200OK)]
	public async Task<ActionResult<ProductDto[]>> GetAll([FromQuery] GetAllProductsQuery request,
		CancellationToken cancellationToken) {
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}

	[HttpPost]
	[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
	public async Task<ActionResult<ProductDto>> Create(CreateProductCommand request,
		CancellationToken cancellationToken) {
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}

	[HttpPut("{id:guid}")]
	[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ProductDto>> Update(Guid id, UpdateProductCommand request,
		CancellationToken cancellationToken) {
		request.Id = id;
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	public async Task<Unit> Delete(Guid id, CancellationToken cancellationToken) {
		var request = new DeleteProductCommand {
			Id = id,
		};
		var result = await _mediator.Send(request, cancellationToken);
		return result;
	}
}