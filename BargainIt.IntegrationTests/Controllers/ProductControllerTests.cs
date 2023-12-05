﻿using System.Net.Http.Json;
using BargainIt.Application.Requests.Products;
using BargainIt.Application.Requests.Products.Commands.CreateProduct;
using BargainIt.Application.Requests.Products.Commands.DeleteProduct;
using BargainIt.Application.Requests.Products.Commands.UpdateProduct;
using BargainIt.Tests.Shared.Seed;

namespace BargainIt.IntegrationTests.Controllers;

public class ProductControllerTests : BaseTest {
	private const string Route = "api/products";

	[Test]
	public async Task Get_WhenDataIsCorrect_ShouldBeOk() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var products = await ApplicationDbContext.Products.ToListAsync();
		// Act
		var response = await HttpClient.GetAsync(Route);
		// Assert
		response.Should().Be200Ok();
		var result = await response.Content.DeserializeContentAsync<IEnumerable<ProductDto>>();
		result.Should().BeEquivalentTo(products, options => options.ExcludingMissingMembers());
	}

	[Test]
	public async Task Post_WhenDataIsCorrect_ShouldBeOk() {
		// Arrange
		var request = new CreateProductCommand {
			Name = "Test",
			Price = 111.11m,
		};
		// Act
		var response = await HttpClient.PostAsJsonAsync(Route, request);
		// Assert
		response.Should().Be200Ok();
		var result = await response.Content.DeserializeContentAsync<ProductDto>();
		result.Should().BeEquivalentTo(request, options => options.ExcludingMissingMembers());
	}

	[Test]
	public async Task Put_WhenEntityNotExist_ShouldBe404NotFound() {
		// Arrange
		var request = new UpdateProductCommand {
			Id = Guid.NewGuid(),
			Name = "Test",
			Price = 111.11m,
		};
		// Act
		var response = await HttpClient.PutAsJsonAsync($"{Route}/{request.Id}", request);
		// Assert
		response.Should().Be404NotFound();
	}

	[Test]
	public async Task Put_WhenDataIsCorrect_ShouldBeOk() {
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var product = await ApplicationDbContext.Products.FirstAsync();
		var request = new UpdateProductCommand {
			Id = product.Id,
			Name = Guid.NewGuid().ToString().Substring(0, 10),
			Price = 111.11m,
		};
		// Act
		var response = await HttpClient.PutAsJsonAsync($"{Route}/{request.Id}", request);
		// Assert
		response.Should().Be200Ok();
		var result = await response.Content.DeserializeContentAsync<ProductDto>();
		result.Should().BeEquivalentTo(request, options => options.ExcludingMissingMembers());
	}

}