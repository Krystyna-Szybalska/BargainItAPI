using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Requests.Products.Commands.UpdateProduct;
using BargainIt.Tests.Shared.Seed;
using BargainIt.UnitTests.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.UnitTests.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandlerTests : BaseRequestTest {
	
	[Test]
	public async Task Handle_ShouldUpdateProduct() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();		
		var request = new UpdateProductCommand {
			Name = "updated name",
			Id = product.Id,
		};
		var sut = new UpdateProductCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = await sut.Handle(request, CancellationToken.None);
		// Assert
		var updatedProduct = await ApplicationDbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
		updatedProduct.Should().BeEquivalentTo(request);
		result.Name.Should().Be(request.Name);
	}
	
	
	[Test]
	public async Task Handle_GivenNonExistingId_ShouldThrowException() {
		// Arrange
		var request = new UpdateProductCommand() {
			Name = "updated name",
			Id = Guid.NewGuid(),
		};
		var sut = new UpdateProductCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<NotFoundException>();
	}
}