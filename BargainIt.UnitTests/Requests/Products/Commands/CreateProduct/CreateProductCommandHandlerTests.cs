using FluentAssertions;
using BargainIt.UnitTests.Factories;
using BargainIt.Application.Requests.Products.Commands.CreateProduct;

namespace BargainIt.UnitTests.Requests.Products.Commands.CreateProduct;

public class CreateProductCommandHandlerTests : BaseRequestTest {
	[Test]
	public async Task Handle_ShouldCreateProduct() {
		// Arrange
		var request = new CreateProductCommand {
			Name = "test product",
			Price = 100,
		};
		var sut = new CreateProductCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = await sut.Handle(request, CancellationToken.None);
		// Assert
		result.Id.Should().NotBeEmpty();
		result.Name.Should().Be(request.Name);
		result.Price.Should().Be(request.Price);
	}
}