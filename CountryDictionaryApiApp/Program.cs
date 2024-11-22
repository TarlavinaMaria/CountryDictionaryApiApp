using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CountryDictionaryApiApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();
var app = builder.Build();

app.MapGet("/", () => new
{
    Message = "server is running"
});

app.MapGet("/ping", () => new
{
    Message = "ping"
});

// countries api
app.MapGet("/country", async (ApplicationDbContext db) => await db.Countries.ToListAsync());
app.MapPost("/country", async (Country country, ApplicationDbContext db) =>
{
    await db.Countries.AddAsync(country);
    await db.SaveChangesAsync();
    return country;
});


app.Run();
