using DemoCoreMVC.DAL;
using DemoCoreMVC.Files;
using DemoCoreMVC.Interfaces;
using DemoCoreMVC.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //具名DI
            builder.Services.AddKeyedSingleton<IFile, FTPFile>("ftp");
            builder.Services.AddKeyedSingleton<IFile, IOFile>("io");
            builder.Services.AddDbContext<AdventureWorksLT2022Context>(
              options => options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value));

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseMiddleware<LogMiddleware>(); // 設定自定義 Middleware
            app.UseMiddleware<LogMiddleware2>(); // 設定自定義 Middleware

            app.Run();
        }
    }
}
