using Serilog;
using BargainIt.Api.Configuration.ApiVersioning;
using BargainIt.Api.Configuration.HealthChecks;
using BargainIt.Api.Configuration.JsonSerilizer;
using BargainIt.Api.Configuration.Logging;
using BargainIt.Api.Configuration.ServicesValidation;
using BargainIt.Api.Configuration.Swagger;
using BargainIt.Application.Extensions;
using BargainIt.Shared.Extensions;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();

Log.Information("Starting up");

try {
	RunApplication();
}


catch (Exception ex) {
	Log.Fatal(ex, "Unhandled exception");
}
finally {
	Log.Information("Shut down complete");
	Log.CloseAndFlush();
}


void RunApplication() {
	var builder = WebApplication.CreateBuilder(args);
	// Logging
	builder.Host.UseSerilog((ctx, lc) => lc
		.Enrich.FromLogContext()
		.WriteTo.Console()
		.ReadFrom.Configuration(ctx.Configuration));
	// Add services to the container.
	builder.Services.AddShared(builder.Configuration);
	builder.Services.AddApplication(builder.Configuration);
	builder.Services.AddHealthChecks(builder.Configuration, builder.Environment);
	builder.Services.AddControllers().AddJsonSerializer();
	builder.Services.AddDefaultApiVersioning();
	builder.Services.AddCors();
	builder.Services.AddSwagger();
	builder.Host.AddServicesValidationOnStart();

	var app = builder.Build();
	app.UseCors(policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
	app.UseSerilogRequestLogging(options => { options.GetLevel = LogHelper.ExcludeHealthChecks; });
	app.UseApplication();
	// Configure the HTTP request pipeline.
	if (!app.Environment.IsProduction()) app.UseSwaggerUi();
	app.MapHealthChecks();
	app.MapControllers();
	app.Run();
}