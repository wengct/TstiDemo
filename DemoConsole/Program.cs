using DemoConsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DemoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            string? param1 = builder.GetSection("param1").Value;
            string? conn = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
                       
            var services = new ServiceCollection(); 
            services.AddDbContext<AdventureWorksLT2022Context>(
                options => options.UseSqlServer(builder.GetSection("ConnectionStrings:DefaultConnection").Value));

            var serviceProvider = services.BuildServiceProvider();
            using (var context = serviceProvider.GetRequiredService<AdventureWorksLT2022Context>())
            {
                var data = context.Product.FirstOrDefault(); // 替換成你的 DbSet 名稱
                Console.WriteLine(data != null ? data.Name : "No data found");
            }

            Console.WriteLine("Setting Value: " + (param1 ?? "Test Empty"));
        }
    }
}
