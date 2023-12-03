using Microsoft.Extensions.DependencyInjection;
using BargainIt.Persistence;

namespace BargainIt.IntegrationTests;

[SetUpFixture]
public class FixtureSetup {
	private static BargainItApiWebApplicationFactory _applicationFactory = default!;
	public static HttpClient HttpClient = default!;
	public static Func<Task> ResetDatabase = default!;
	public static Func<ApplicationDbContext> GetApplicationDbContext = default!;

	[OneTimeSetUp]
	public async Task OneTimeSetup() {
		_applicationFactory = new BargainItApiWebApplicationFactory();
		await _applicationFactory.InitializeAsync();
		
		HttpClient = _applicationFactory.HttpClient;
		ResetDatabase = _applicationFactory.ResetDatabase;
		GetApplicationDbContext = CreateApplicationDbContext;
	}

	[OneTimeTearDown]
	public async Task OneTimeTearDown() {
		await _applicationFactory.DisposeAsync();
	}

	private static ApplicationDbContext CreateApplicationDbContext() {
		var context = _applicationFactory.Services.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
		                                 .CreateDbContext();
		context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		return context;
	}
}