using BargainIt.Application.Requests.Products.Commands.CreateProduct;
using FluentValidation.TestHelper;

namespace BargainIt.UnitTests.Requests.Products.Commands.CreateProduct; 

public class CreateProductCommandValidatorTests {
	private CreateProductCommandValidator _sut = null!;
	
	[SetUp]
	public void Setup() {
		_sut = new CreateProductCommandValidator();
	}
	
	[Test]
	public void Validate_GivenPriceZero_ShouldFail() {
		// Arrange
		var request = new CreateProductCommand {
			Name = "test",
			Price = 0,
		};
		// Act
		var response = _sut.TestValidate(request);
		// Assert
		response.ShouldHaveValidationErrorFor(x => x.Price).Only();
	}
}