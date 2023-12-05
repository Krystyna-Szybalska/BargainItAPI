using BargainIt.Persistence;

namespace BargainIt.IntegrationTests;

public class BaseTest {
	protected static HttpClient HttpClient => FixtureSetup.HttpClient;
	protected ApplicationDbContext ApplicationDbContext = null!;

	[SetUp]
	public void Setup() {
		ApplicationDbContext = FixtureSetup.GetApplicationDbContext();
	}

	[TearDown]
	public virtual async Task TearDown() {
		await ApplicationDbContext.DisposeAsync();
		await FixtureSetup.ResetDatabase();
	}
}