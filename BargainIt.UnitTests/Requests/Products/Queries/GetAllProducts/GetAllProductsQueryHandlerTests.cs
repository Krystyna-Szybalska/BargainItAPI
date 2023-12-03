﻿using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using BargainIt.Application.Requests.Products.Queries.GetAllProducts;
using BargainIt.Tests.Shared.Seed;
using BargainIt.UnitTests.Factories;

namespace BargainIt.UnitTests.Requests.Products.Queries.GetAllProducts;

[TestFixture]
public class GetAllProductsQueryHandlerTests : BaseRequestTest
{
	private Fixture _fixture = null!;

	[SetUp]
	public void Setup()
	{
		_fixture = new Fixture();
	}

	[TearDown]
	public void TearDown()
	{
	}

	[Test]
	public async Task Handle_NoData_ShouldBeSuccess()
	{
		// Arrange
		await ApplicationDbContext.SeedWithAsync<ProductSeed>();
		var products = await ApplicationDbContext.Products.ToListAsync();
		var request = _fixture.Create<GetAllProductsQuery>();
		var sut = new GetAllProductsQueryHandler(ApplicationDbContext, MapperFactory.Mapper);
		// Act
		var result = await sut.Handle(request, CancellationToken.None);
		// Assert
		result.Should().HaveSameCount(products);
	}
}