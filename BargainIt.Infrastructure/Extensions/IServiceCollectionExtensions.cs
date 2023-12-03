using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BargainIt.Application.Services.Emails;
using BargainIt.Infrastructure.Services.Emails;

namespace BargainIt.Infrastructure.Extensions; 

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions {
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IEmailService, EmailService>();
		return services;
	}
}