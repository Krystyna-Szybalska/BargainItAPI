using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Requests.Products.Commands.DeleteProduct;
using BargainIt.Tests.Shared.Seed;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.UnitTests.Requests.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandlerTests : BaseRequestTest {
	[Test]
	public async Task Handle_ShouldDeleteProduct() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();
		var request = new DeleteProductCommand() {
			Id = product.Id,
		};
		var sut = new DeleteProductCommandHandler(ApplicationDbContext);
		// Act
		await sut.Handle(request, CancellationToken.None);
		// Assert
		ApplicationDbContext.Products.Should().NotContain(p => p.Id == product.Id);
	}

	[Test]
	public async Task Handle_GivenNonExistingId_ShouldThrowException() {
		// Arrange
		var request = new DeleteProductCommand() {
			Id = Guid.NewGuid(),
		};
		var sut = new DeleteProductCommandHandler(ApplicationDbContext);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<NotFoundException>();
	}
}