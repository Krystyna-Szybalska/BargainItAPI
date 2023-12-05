using BargainIt.Application.Requests.Negotiations;
using BargainIt.Application.Requests.Negotiations.Commands.CreateNegotiation;
using BargainIt.Application.Requests.Negotiations.Commands.ResolveNegotiation;
using BargainIt.Tests.Shared.Seed;
using Microsoft.AspNetCore.Http.Extensions;

namespace BargainIt.IntegrationTests.Controllers; 

public class NegotiationsControllerTests : BaseTest{
	private const string Route = "api/negotiations";
	
	[Test]
	public async Task Get_WhenDataIsCorrect_ShouldBeOk() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<NegotiationSeed>();
		var negotiations = ApplicationDbContext.Negotiations
			.AsEnumerable()
			.GroupBy(n=>n.ProductId)
			.First();
		var productId = negotiations.Key;

		var queryBuilder = new QueryBuilder();
		queryBuilder.Add("productId", productId.ToString());
		
		// Act
		var response = await HttpClient.GetAsync($"{Route}{queryBuilder}");
		// Assert
		response.Should().Be200Ok();
		var result = await response.Content.DeserializeContentAsync<IEnumerable<NegotiationDto>>();
		result.Should().BeEquivalentTo(negotiations, options => options.ExcludingMissingMembers());
	}

	[Test]
	public async Task Post_WhenDataIsCorrect_ShouldBeOk() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();
		var request = new CreateNegotiationCommand {
			ProposedPrice = product.Price,
			ProductId = product.Id
		};
		
		// Act
		var response = await HttpClient.PostAsJsonAsync(Route, request);
		// Assert
		response.Should().Be200Ok();
		var result = await response.Content.DeserializeContentAsync<NegotiationDto>();
		result.Should().BeEquivalentTo(request, options => options.ExcludingMissingMembers());
	}

	[Test]
	public async Task Put_WhenEntityNotExist_ShouldBe404NotFound() {
		// Arrange
		var request = new ResolveNegotiationCommand {
			Id = Guid.NewGuid(),
			IsAccepted = false,
		};
		// Act
		var response = await HttpClient.PutAsJsonAsync($"{Route}/{request.Id}", request);
		// Assert
		response.Should().Be404NotFound();
	}

	[Test]
	public async Task Put_WhenDataIsCorrect_ShouldBeOk() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<NegotiationSeed>();
		var negotiation = await ApplicationDbContext.Negotiations.FirstAsync();
		var request = new ResolveNegotiationCommand {
			Id = negotiation.Id,
			IsAccepted = false,
		};
		// Act
		var response = await HttpClient.PutAsJsonAsync($"{Route}/{request.Id}/resolve", request);
		// Assert
		response.Should().Be200Ok();
	} 
}