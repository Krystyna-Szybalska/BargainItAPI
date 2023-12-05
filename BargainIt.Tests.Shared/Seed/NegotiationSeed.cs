using BargainIt.Persistence;
using BargainIt.Persistence.Entities.Negotations;
using Microsoft.EntityFrameworkCore;

namespace BargainIt.Tests.Shared.Seed;

public class NegotiationSeed : BaseSeed {
	public NegotiationSeed(ApplicationDbContext context) : base(context) { }

	public override async Task SeedAsync() {
		if (!Context.Products.Any()) await Context.SeedWithAsync<ProductSeed>();

		var product = await Context.Products.FirstAsync();
		var negotiation = new NegotiationEntity {
			Id = Guid.NewGuid(),
			ProposedPrice = (decimal)(Random.Shared.NextDouble() * 10000),
			ProductId = product.Id,
		};

		Context.Negotiations.Add(negotiation);
		await Context.SaveChangesAsync();
	}
}