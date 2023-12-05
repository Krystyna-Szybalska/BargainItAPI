using BargainIt.Application.Requests.Products.Commands.UpdateProduct;
using FluentValidation.TestHelper;

namespace BargainIt.UnitTests.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidatorTests {

	private UpdateProductCommandValidator _sut = null!;
	
	[SetUp]
	public void Setup() {
		_sut = new UpdateProductCommandValidator();
	}
	
	[Test]
	public void Validate_GivenEmptyName_ShouldFail() {
		// Arrange
		var request = new UpdateProductCommand {
			Id = default,
			Name = String.Empty,
			Price = 1,
		};
		// Act
		var response = _sut.TestValidate(request);
		// Assert
		response.ShouldHaveValidationErrorFor(x => x.Name).Only();
	}


	[Test]
	public void Validate_GivenPriceZero_ShouldFail() {
		// Arrange
		var request = new UpdateProductCommand {
			Id = default,
			Name = "test",
			Price = 0,
		};
		// Act
		var response = _sut.TestValidate(request);
		// Assert
		response.ShouldHaveValidationErrorFor(x => x.Price).Only();
	}
}