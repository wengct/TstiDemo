using DemoConsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                options =>
                {
                    options.UseSqlServer(builder.GetSection("ConnectionStrings:DefaultConnection").Value);
                    options.EnableSensitiveDataLogging(); // 啟用敏感數據日誌
                    options.EnableDetailedErrors(); // 啟用詳細錯誤
                    //options.LogTo(Console.WriteLine, LogLevel.Information); // 將 SQL 輸出到控制台，如果appsetting.json有寫，這邊就不用了
                });
            services.AddLogging(configure => configure.AddConsole());


            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            //using (var context = serviceProvider.GetRequiredService<AdventureWorksLT2022Context>())
            //{
            //    var data = context.Product.FirstOrDefault(); // 替換成你的 DbSet 名稱
            //    Console.WriteLine(data != null ? data.Name : "No data found");
            //}
            //Console.WriteLine("Setting Value: " + (param1 ?? "Test Empty"));
            var db = serviceProvider.GetRequiredService<AdventureWorksLT2022Context>();
            //Query(db);
            //Query指定單個欄位(db);
            //Query指定多個欄位(db);
            //Query指定多個欄位放在指定物件(db);
            //Query多筆資料(db);
            //Query多筆資料指定多個欄位(db);
            //Query多筆資料篩選不好的寫法(db);
            //Query多筆資料篩選良好的寫法(db);
            //Insert(db);
            //Insert沒有給必填欄位(db);
            //Update(db);
            //Update不先查詢(db);
            //Delete(db);
            //Delete不先查詢(db);
        }

        private static void Delete不先查詢(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Delete不先查詢");
            Address address = new Address
            {
                AddressID = 11383
            };
            db.Address.Attach(address);
            db.Address.Remove(address);
            db.SaveChanges();
        }

        private static void Delete(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Delete");
            Address address = new Address
            {
                AddressLine1 = "123 Main Street",
                City = "Dallas",
                StateProvince = "TX",
                CountryRegion = "US",
                PostalCode = "75201",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };
            db.Address.Add(address);

            db.Address.Remove(address);
            db.SaveChanges();
        }

        private static void Update不先查詢(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Update不先查詢");
            Address? address = new Address
            {
                AddressID = 11383,
                AddressLine2 = null //
            };
            db.Address.Attach(address);
            if (address != null)
            {
                address.AddressLine2 = "Test2";
                db.SaveChanges();
            }
            Console.WriteLine(address?.AddressLine1 ?? "查無資料");
        }

        private static void Update(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Update");
            Address? address = db.Address.FirstOrDefault();
            if (address != null)
            {
                address.AddressLine2 = "Test2";
                db.SaveChanges();
            }
            Console.WriteLine(address?.AddressLine1 ?? "查無資料");
        }

        private static void Insert沒有給必填欄位(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Insert");
            Address address = new Address
            {
                AddressLine1 = "234 Main Street",
                City = "Dallas",
                StateProvince = "TX",
                CountryRegion = "US",
                //PostalCode = "75201",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };
            db.Address.Add(address);
            Console.WriteLine($"新增前，AddressId = {address.AddressID}");
            db.SaveChanges();
            Console.WriteLine($"新增後，AddressId = {address.AddressID}");
        }

        private static void Insert(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Insert");
            Address address = new Address
            {
                AddressLine1 = "123 Main Street",
                City = "Dallas",
                StateProvince = "TX",
                CountryRegion = "US",
                PostalCode = "75201",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };
            db.Address.Add(address);

            Console.WriteLine($"新增前，AddressId = {address.AddressID}");
            db.SaveChanges();
            Console.WriteLine($"新增後，AddressId = {address.AddressID}");
        }

        private static void Query多筆資料篩選良好的寫法(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query多筆資料篩選良好的寫法");
            var addresses = db.Address
                                      //.ToList()    //不要先ToList()
                                      .Where(c => c.City == "Dallas")
                                      .Select(c => new { c.AddressLine1, c.City })
                                      .ToList();
            foreach (var item in addresses)
            {
                Console.WriteLine($"AddressLine1 = {item.AddressLine1}\tCity = {item.City}");
            }
        }

        private static void Query多筆資料篩選不好的寫法(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query多筆資料篩選不好的寫法");
            var addresses = db.Address.ToList()
                                      .Where(c => c.City == "Dallas")
                                      .Select(c => new { c.AddressLine1, c.City })
                                      .ToList();
            foreach (var item in addresses)
            {
                Console.WriteLine($"AddressLine1 = {item.AddressLine1}\tCity = {item.City}");
            }
        }

        private static void Query多筆資料指定多個欄位(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query多筆資料指定多個欄位");
            var addresses = db.Address.Where(c => c.City == "Dallas")
                                      .Select(c => new { c.AddressLine1, c.City })
                                      .ToList();
            foreach (var item in addresses)
            {
                Console.WriteLine($"AddressLine1 = {item.AddressLine1}\tCity = {item.City}");
            }
        }

        private static void Query多筆資料(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query多筆資料");
            List<Address>? addresses = db.Address.Where(c => c.City == "Dallas").ToList();
            foreach (var item in addresses)
            {
                Console.WriteLine($"AddressLine1 = {item.AddressLine1}");
            }
        }

        private static void Query指定多個欄位放在指定物件(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query指定多個欄位放在指定物件");
            var customAddress = db.Address.Select(c => new CustomAddress
            {
                AddressLine1 = c.AddressLine1,
                City = c.City
            }).FirstOrDefault();
            Console.WriteLine($"{customAddress!.AddressLine1} + {customAddress!.City}");
        }

        private static void Query指定多個欄位(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query指定多個欄位");
            var addressLine = db.Address.Select(c => new { c.AddressLine1, c.AddressLine2 }).FirstOrDefault();
            Console.WriteLine($"{addressLine!.AddressLine1} + {addressLine!.AddressLine2}");
        }

        private static void Query指定單個欄位(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query指定單個欄位");
            string? addressLine1 = db.Address.Select(c => c.AddressLine1).FirstOrDefault();
            Console.WriteLine(addressLine1 ?? "查無資料");
        }

        private static void Query(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("Query");
            Address? address = db.Address.FirstOrDefault();
            Console.WriteLine(address?.AddressLine1 ?? "查無資料");
        }
    }

    internal class CustomAddress
    {
        public string? AddressLine1 { get; set; }
        public string? City { get; set; }
    }
}
