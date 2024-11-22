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

// Получение страны по коду
app.MapGet("/country/alpha2/{code}", async (string code, ApplicationDbContext db) =>
{
    var country = await db.Countries.FirstOrDefaultAsync(c => c.ISO31661Alpha2Code == code);
    return country != null ? Results.Ok(country) : Results.NotFound();
});

app.MapGet("/country/alpha3/{code}", async (string code, ApplicationDbContext db) =>
{
    var country = await db.Countries.FirstOrDefaultAsync(c => c.ISO31661Alpha3Code == code);
    return country != null ? Results.Ok(country) : Results.NotFound();
});

app.MapGet("/country/numeric/{code}", async (string code, ApplicationDbContext db) =>
{
    var country = await db.Countries.FirstOrDefaultAsync(c => c.ISO31661NumericCode == code);
    return country != null ? Results.Ok(country) : Results.NotFound();
});

// Получение страны по id
app.MapGet("/country/{id}", async (int id, ApplicationDbContext db) =>
{
    var country = await db.Countries.FindAsync(id);
    return country != null ? Results.Ok(country) : Results.NotFound();
});

app.Run();
