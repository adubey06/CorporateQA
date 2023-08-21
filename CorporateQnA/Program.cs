using CorporateQnA.Helpers;
using CorporateQnA.Interfaces;
using CorporateQnA.Models;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using CorporateQnA.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContextPool<AppDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddServices();

builder.Services.AddTransient<IDbConnection>((connection) => new SqlConnection(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddScoped<IRequestContext, RequestContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "490082799116-c5jem6d5gfuokc500f6nhm7c4qfajdp9.apps.googleusercontent.com";
    options.ClientSecret = "_gpk67ROxx2OfYWczWxQ6uAR";
});
//builder.Services.AddMvc();
//builder.Services.AddRazorPages();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
