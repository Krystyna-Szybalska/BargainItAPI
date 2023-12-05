using BargainIt.Application.Requests.Negotiations.Commands.CreateNegotiation;
using FluentValidation.TestHelper;

namespace BargainIt.UnitTests.Requests.Negotiations.Commands.CreateNegotiation; 

public class CreateNegotiationCommandValidatorTests {
	private CreateNegotiationCommandValidator _sut = null!;
	
	[SetUp]
	public void Setup() {
		_sut = new CreateNegotiationCommandValidator();
	}
	
	[Test]
	public void Validate_GivenPriceZero_ShouldFail() {
		// Arrange
		var request = new CreateNegotiationCommand {
			ProposedPrice = 0,
			ProductId = default,
		};
		// Act
		var response = _sut.TestValidate(request);
		// Assert
		response.ShouldHaveValidationErrorFor(x => x.ProposedPrice).Only();
	}
}