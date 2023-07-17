using GateKeeper.Interfaces;
using GateKeeper.Users;
using GateKeeper.Users.BaseClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Librarian.Interfaces;
using Librarian.Contexts;
using XPros_Stock_And_Inventory.Areas.Identity.Pages.Account;
using System.Data.SqlClient;
using Librarian;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

#region Page Authorizations
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Index");
    options.Conventions.AuthorizePage("/Privacy");
});
#endregion

#region Authentication Injections
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddScoped<IUser<Guid>, AdminFormsUser>();
builder.Services.AddScoped<IUserInfo<Guid>, UserInput>();
builder.Services.AddScoped<IContext, UserContext>();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/Account/Login");
#endregion

#region DataOperation Injections
builder.Services.AddScoped<IDataContext<SqlConnection, SqlCommand>, SqlDataContext>(options => 
{
    return new SqlDataContext(connectionString);
});
builder.Services.AddScoped<IDataOperator, SqlDataOperator>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
