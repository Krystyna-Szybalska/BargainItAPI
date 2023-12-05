using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BargainIt.Shared.Services.DateTimeProviders;

namespace BargainIt.Shared.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
	public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IDateTimeProvider, DateTimeProvider>();
		return services;
	}
}