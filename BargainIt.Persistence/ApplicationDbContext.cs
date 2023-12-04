using System.Reflection;
using BargainIt.Persistence.Entities.Negotations;
using Microsoft.EntityFrameworkCore;
using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Persistence;

public class ApplicationDbContext : DbContext {
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<ProductEntity> Products => Set<ProductEntity>();
	public DbSet<NegotiationEntity> Negotiations => Set<NegotiationEntity>();

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}