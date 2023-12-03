using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BargainIt.Persistence.Entities.Products;

namespace BargainIt.Persistence;

public class ApplicationDbContext : DbContext {
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<ProductEntity> Products => Set<ProductEntity>();

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}