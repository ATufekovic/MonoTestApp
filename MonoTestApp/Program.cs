﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonoTestApp.Data;
using MonoTestApp.Project.Service;
using MonoTestApp.Project.Service.Controllers;
using MonoTestApp.Project.Service.DevelopmentTools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MonoTestAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MonoTestAppContext") ?? throw new InvalidOperationException("Connection string 'MonoTestAppContext' not found.")));

var app = builder.Build();

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

app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();

app.Run();