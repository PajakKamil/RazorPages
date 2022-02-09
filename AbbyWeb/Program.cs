using System.Runtime.CompilerServices;
using Abby.DataAccess.Repository.IRepository;
using AbbyWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Abby.DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Abby.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
	builder.Configuration.GetConnectionString("DefaultConnection")
	));
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)		//Domyœlne ustawienia Identity
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()          //Dodaæ do tworzenia ról u¿ytkownikoów
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();		
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
