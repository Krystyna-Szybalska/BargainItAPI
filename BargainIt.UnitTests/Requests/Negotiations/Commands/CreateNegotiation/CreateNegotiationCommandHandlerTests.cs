using BargainIt.Application.Behaviour.Exceptions;
using BargainIt.Application.Requests.Negotiations.Commands.CreateNegotiation;
using BargainIt.Persistence.Entities.Negotations;
using BargainIt.Tests.Shared.Seed;
using BargainIt.UnitTests.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.UnitTests.Requests.Negotiations.Commands.CreateNegotiation; 

public class CreateNegotiationCommandHandlerTests : BaseRequestTest {
	[Test]
	public async Task Handle_ShouldCreateNegotiation() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();
		var request = new CreateNegotiationCommand {
			ProposedPrice = product.Price,
			ProductId = product.Id,
		};
		var sut = new CreateNegotiationCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = await sut.Handle(request, CancellationToken.None);
		// Assert
		result.Id.Should().NotBeEmpty();
		result.ProductId.Should().Be(request.ProductId);
		result.ProposedPrice.Should().Be(request.ProposedPrice);
	}
	
	[Test]
	public async Task Handle_NonExistingProduct_ShouldThrowException() {
		// Arrange
		var request = new CreateNegotiationCommand {
			ProposedPrice = 10,
			ProductId = Guid.NewGuid(),
		};
		var sut = new CreateNegotiationCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<NotFoundException>();
	}
	
	[Test]
	public async Task Handle_ProposedPriceTooHigh_ShouldThrowException() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();
		var request = new CreateNegotiationCommand {
			ProposedPrice = product.Price * 2 + 1,
			ProductId = product.Id,
		};
		var sut = new CreateNegotiationCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<VerificationException>();
	}
	
	[Test]
	public async Task Handle_NegotiationAlreadyAccepted_ShouldThrowException() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();
		product.Negotiations.Add(new NegotiationEntity {
			ProposedPrice = product.Price,
			IsAccepted = true,
		});
		await ApplicationDbContext.SaveChangesAsync();
		var request = new CreateNegotiationCommand {
			ProposedPrice = product.Price,
			ProductId = product.Id,
		};
		var sut = new CreateNegotiationCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<VerificationException>();
	}
	
	[Test]
	public async Task Handle_NegotiationLimitExceeded_ShouldThrowException() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.Include(productEntity => productEntity.Negotiations).FirstAsync();
		product.Negotiations = new List<NegotiationEntity> {
			new() { ProposedPrice = product.Price, IsAccepted = false, },
			new() { ProposedPrice = product.Price, IsAccepted = false, },
			new() { ProposedPrice = product.Price, IsAccepted = false, },
		};
		await ApplicationDbContext.SaveChangesAsync();
		var request = new CreateNegotiationCommand {
			ProposedPrice = product.Price,
			ProductId = product.Id,
		};
		var sut = new CreateNegotiationCommandHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = () => sut.Handle(request, CancellationToken.None);
		// Assert
		await result.Should().ThrowAsync<VerificationException>();
	}
	
}