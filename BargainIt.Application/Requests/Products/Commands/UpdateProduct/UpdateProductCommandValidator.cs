using FluentValidation;

namespace BargainIt.Application.Requests.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand> {
	public UpdateProductCommandValidator() {
		RuleFor(x => x.Name).NotEmpty();
		RuleFor(x => x.Price).GreaterThan(0);
	}
}