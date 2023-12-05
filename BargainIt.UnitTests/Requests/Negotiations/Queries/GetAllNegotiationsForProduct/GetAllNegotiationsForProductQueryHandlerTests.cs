using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Requests.Negotiations.Queries.GetAllNegotiationsForProduct;
using BargainIt.Tests.Shared.Seed;
using BargainIt.UnitTests.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.UnitTests.Requests.Negotiations.Queries.GetAllNegotiationsForProduct; 

public class GetAllNegotiationsForProductQueryHandlerTests : BaseRequestTest{
	[Test]
	public async Task Handle_ExistingProduct_ShouldBeSuccess() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products
			.Include(productEntity => productEntity.Negotiations)
			.FirstAsync();
		var request = new GetAllNegotiationsForProductQuery {
			ProductId = product.Id,
		};
		var sut = new GetAllNegotiationsForProductQueryHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = await sut.Handle(request, CancellationToken.None);
		// Assert
		result.Should().HaveSameCount(product.Negotiations);
	}
	
	[Test]
	public async Task Handle_NonExistingProduct_ShouldThrowError() {
		// Arrange
		var request = new GetAllNegotiationsForProductQuery {
			ProductId = Guid.NewGuid(),
		};
		var sut = new GetAllNegotiationsForProductQueryHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<NotFoundException>();
	}
}