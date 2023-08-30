using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TamansShop.Areas.Identity.Data;
using TamansShop.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TamansShopContextConnection") ?? throw new InvalidOperationException("Connection string 'TamansShopContextConnection' not found.");

builder.Services.AddDbContext<TamansShopContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<TamansShopUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<TamansShopContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.AddConsole(); // Add console logging provider
builder.Logging.AddDebug();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();