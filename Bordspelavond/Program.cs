using Bordspelavond.Infrastructure.Repo;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bordspelavond.Data;
using Bordspelavond.IdentityObject;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityDBContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityDBContextConnection' not found.");

builder.Services.AddDbContext<IdentityDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContext<DomainContext>(options =>
{
    options.UseSqlServer(@"Data Source=avanssswp2022bordspel.database.windows.net;Initial Catalog=SSWPDatabase;Persist Security Info=True;User ID=jascha-van-der-ark;Password=khg36d8Q2NAsBDd");
    
});

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.SignIn.RequireConfirmedAccount = false;

    })
    .AddEntityFrameworkStores<IdentityDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdultsOnly", policy => policy.RequireClaim("EighteenPlus"));
    options.AddPolicy("Organizer", policy => policy.RequireClaim("Organizer"));
    
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBoardGameRepo, EFGameRepo>();
builder.Services.AddScoped<IGameNightRepo, EFGameNightRepo>();
builder.Services.AddScoped<IWebsiteUserRepo, EFWebsiteUserRepo>();

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
app.UseAuthentication();;
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

