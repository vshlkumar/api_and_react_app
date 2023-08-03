using EntityFrameworkUI.Models;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Refit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

builder.Services.AddSpaStaticFiles(x =>
{
    x.RootPath = "Client/build";
});

builder.Services.AddAuthorization();


builder.Services.AddRefitClient<IToken>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://localhost:7235/api/account");
    })
    .ConfigurePrimaryHttpMessageHandler<TokenHandler>();

builder.Services.AddTransient<TokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseMiddleware<ProxyMiddleware>();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();    
});


app.UseSpa(spa =>
{
    spa.Options.SourcePath = "Client/my-app";
    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
        spa.UseProxyToSpaDevelopmentServer("http://localhost:21043");
    }
});

app.Run();
