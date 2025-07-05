using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Notification;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDataContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));
builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
builder.Services.AddTransient<IDashboardRepo, DashboardRepo>();
builder.Services.AddTransient<ITaskRepo, TaskRepo>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<OverdueTaskNotifier>();

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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
