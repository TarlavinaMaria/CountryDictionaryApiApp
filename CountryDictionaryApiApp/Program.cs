using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new
{
    Message = "server is running"
});

app.MapGet("/ping", () => new
{
    Message = "ping"
});

app.Run();
