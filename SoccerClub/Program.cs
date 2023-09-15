using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoccerClub.Controllers;
using SoccerClub.Models;
using System;
using Microsoft.Owin.Security.Cookies;
using ApplicationUser = SoccerClub.Controllers.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));
builder.Services.AddScoped<ProductService>();
builder.Services.AddDistributedMemoryCache(); // Use an in-memory cache for session data
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set the session timeout
});

builder.Services.AddAuthentication("MyCookieScheme")
        .AddCookie("MyCookieScheme", options =>
        {
            options.Cookie.Name = "MyAppCookie";
            options.LoginPath = "/UserLogin/Index"; // Customize the login path
            // Other cookie authentication options
        });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(); // Add session support
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
/*

builder.Services.AddScoped<UserManager<SoccerClub.Controllers.ApplicationUser>>(); // Add this line
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}/{id?}");

app.Run();
