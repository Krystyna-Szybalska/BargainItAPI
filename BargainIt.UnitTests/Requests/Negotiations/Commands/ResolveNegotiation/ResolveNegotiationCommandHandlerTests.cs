using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation;
using BargainIt.Tests.Shared.Seed;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.UnitTests.Requests.Negotiations.Commands.ResolveNegotiation; 

public class ResolveNegotiationCommandHandlerTests : BaseRequestTest{
	[Test]
	public async Task Handle_ShouldResolveNegotiation() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<NegotiationSeed>();
		var negotiation = await ApplicationDbContext.Negotiations.FirstAsync();		
		var request = new ResolveNegotiationCommand {
			Id = negotiation.Id,
			IsAccepted = false,
		};
		var sut = new ResolveNegotiationCommandHandler(ApplicationDbContext);
		// Act
		await sut.Handle(request, CancellationToken.None);
		// Assert
		var resolvedNegotiation = await ApplicationDbContext.Negotiations
			.FirstOrDefaultAsync(p => p.Id == negotiation.Id);
		resolvedNegotiation.Should().BeEquivalentTo(request);
	}
	
	
	[Test]
	public async Task Handle_GivenNonExistingId_ShouldThrowException() {
		// Arrange
		var request = new ResolveNegotiationCommand {
			Id = Guid.NewGuid(),
			IsAccepted = false,
		};
		var sut = new ResolveNegotiationCommandHandler(ApplicationDbContext);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<NotFoundException>();
	}
}