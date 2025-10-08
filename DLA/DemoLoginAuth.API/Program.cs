using DemoLoginAuth.Infraestructure.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add services to the container.




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
	swagger.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "ASP .NET 8 Web API",
		Description = "Authentication com Json Web Token"
	});

	//Trecho referente a habilitar autorização com swagger(JWT)
	swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});

	swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			}, Array.Empty<string>()
		}
	});
});

builder.Services.AddDIServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors(policy =>
	{
		policy.WithOrigins("https://localhost:7027")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.WithHeaders(HeaderNames.ContentType);
	});
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
