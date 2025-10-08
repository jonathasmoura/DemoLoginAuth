using Blazored.LocalStorage;
using DemoLoginAuth.AppbBlazor.Components;
using DemoLoginAuth.AppbBlazor.States;
using DemoLoginAuth.Application.Services.Authentication;
using DemoLoginAuth.Application.Services.Implements;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped(x => new HttpClient { BaseAddress = new Uri("https://localhost:7001") });
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
