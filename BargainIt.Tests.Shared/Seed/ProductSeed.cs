using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Tests.Shared.Seed;

public class ProductSeed : BaseSeed {
	public ProductSeed(ApplicationDbContext context) : base(context) { }

	public override async Task SeedAsync() {
		var product = new ProductEntity() {
			Id = Guid.NewGuid(),
			Name = Guid.NewGuid().ToString().Substring(0, 10),
			Price = (decimal)(Random.Shared.NextDouble() * 10000),
		};

		Context.Products.Add(product);
		await Context.SaveChangesAsync();
	}
}