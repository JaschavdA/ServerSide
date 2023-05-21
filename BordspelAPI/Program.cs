using System.Text;
using BordspelAPI;
using Bordspelavond.Data;
using Bordspelavond.IdentityObject;
using Bordspelavond.Infrastructure.Repo;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<DomainContext>(options =>
    options.UseSqlServer(
        @"Data Source=avanssswp2022bordspel.database.windows.net;Initial Catalog=SSWPDatabase;Persist Security Info=True;User ID=jascha-van-der-ark;Password=khg36d8Q2NAsBDd"));

builder.Services.AddDbContext<IdentityDBContext>(options =>
{
    options.UseSqlServer(@"Server=tcp:avanssswp2022bordspel.database.windows.net,1433;Initial Catalog=SSWPIdentityDB;Persist Security Info=False;User ID=jascha-van-der-ark;Password=khg36d8Q2NAsBDd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
});

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>{
        config.SignIn.RequireConfirmedAccount = false;

    })
    .AddEntityFrameworkStores<IdentityDBContext>().AddDefaultTokenProviders();


builder.Services.AddControllers();
builder.Services.AddScoped<IBoardGameRepo, EFGameRepo>();
builder.Services.AddScoped<IGameNightRepo, EFGameNightRepo>();
builder.Services.AddScoped<IWebsiteUserRepo, EFWebsiteUserRepo>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer().ModifyRequestOptions(o => o.IncludeExceptionDetails = true).AddQueryType<Query>();


var app = builder.Build();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();
app.Run();
