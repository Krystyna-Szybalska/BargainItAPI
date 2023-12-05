using FluentValidation;

namespace BargainIt.Application.Requests.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
	public CreateProductCommandValidator() {
		RuleFor(x => x.Name).NotEmpty();
		RuleFor(x => x.Price).GreaterThan(0);
	}
}