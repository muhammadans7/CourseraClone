using courseraClone.Components;
using courseraClone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using courseraClone.Data;
using courseraClone.Models;
// using YourProjectamespace.Data; // Replace with your actual namespace


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// builder.Services.AddIdentity<ApplicationUser, IdentityRole>();

builder.Services.AddTransient<UserService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<courseraClone.Services.CourseService>();
builder.Services.AddScoped<DropCourseService>();
builder.Services.AddScoped<ComplaintService>();
builder.Services.AddScoped<AccountService>();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
