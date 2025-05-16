using LibraryManagement.Data;
using LibraryManagement.Repository;
using LibraryManagement.Repository.Contracts;
using LibraryManagement.Services;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LibraryManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Dependency injection
            builder.Services.AddDbContext<LibraryManagementDbContext>();

            builder.Services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

            builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();

            builder.Services.AddScoped<IAuthorManagementService, AuthorManagementService>();
            builder.Services.AddScoped<IBookManagementService, BookManagementService>();
            builder.Services.AddScoped<IUserManagementService, UserManagementService>();
            builder.Services.AddScoped<ILoanManagementService, LoanManagementService>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Accounts/Login";
                    options.AccessDeniedPath = "/Accounts/AccessDenied";
                });

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
