using MediatR;
using Microsoft.EntityFrameworkCore;
using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Application.Requests.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand> {
	private readonly ApplicationDbContext _context;

	public DeleteProductCommandHandler(ApplicationDbContext context) {
		_context = context;
	}

	public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
		var product = await _context.Products
			.AsTracking()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (product is null) throw new NotFoundException(typeof(ProductEntity), request.Id.ToString());

		_context.Products.Remove(product);
		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}