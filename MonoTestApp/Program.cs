using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MonoTestApp.Data;
using MonoTestApp.Project.DevelopmentTools;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MonoTestAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MonoTestAppContext") ?? throw new InvalidOperationException("Connection string 'MonoTestAppContext' not found.")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

var app = builder.Build();

//DI container
var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterAutoMapper(typeof(Program).Assembly);
var container = containerBuilder.Build();
DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

//Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DatabaseSeeder.InitialiseSeedingMakes(services);
    DatabaseSeeder.InitialiseSeedingModels(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
