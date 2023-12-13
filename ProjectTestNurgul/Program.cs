using Common;
using Common.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectTestNurgul.Helpers;
using ProjectTestNurgul.Middleware;
using ProjectTestNurgul.Services.Abstract;
using ProjectTestNurgul.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TestDataContext>(
    options => options.UseMySql(connectionString
        , ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IFileWorker, PdfWorker>();
var app = builder.Build();
app.UseMiddleware<CustomExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

