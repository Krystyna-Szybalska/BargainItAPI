using BargainIt.Application.Requests.Negotiations.Commands.CreateCommand;
using FluentValidation;

namespace BargainIt.Application.Requests.Negotiations.Commands.CreateNegotiation; 

public class CreateNegotiationCommandValidator : AbstractValidator<CreateNegotiationCommand> {
	public CreateNegotiationCommandValidator() {
		RuleFor(x => x.ProposedPrice).GreaterThan(0);
	}
};