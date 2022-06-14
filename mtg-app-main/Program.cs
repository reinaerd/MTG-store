using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mtg_app.Data;
using Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
                        options.SignIn.RequireConfirmedAccount = true;

                        // Require settings
                        options.Password.RequireDigit = true; 
                        options.Password.RequiredLength = 8; 
                        options.Password.RequireNonAlphanumeric = false; 
                        options.Password.RequireUppercase = true; 
                        options.Password.RequireLowercase = false; 
                        options.Password.RequiredUniqueChars = 6;

                        // Lockout settings
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); 
                        options.Lockout.MaxFailedAccessAttempts = 10; 
                        options.Lockout.AllowedForNewUsers = true;
                        // User settings
                        options.User.RequireUniqueEmail = true;
                        
                    }
)   .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shop}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();





