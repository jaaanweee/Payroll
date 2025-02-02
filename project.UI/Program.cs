using Microsoft.AspNetCore.Identity;
using project.Data.DataAccess;
using project.Data.Repository;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add custom configuration source for reading from web.config
builder.Configuration.AddXmlFile("web.config", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IAllowanceRepository, AllowanceRepository>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ILeaveRepository, LeaveRepository>();
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();

// ** Add session services **
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Ensure the "receipts" directory exists
var receiptsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "receipts");
if (!Directory.Exists(receiptsFolder))
{
    Directory.CreateDirectory(receiptsFolder);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** Enable session before authentication **
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
