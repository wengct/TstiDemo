using DemoConsole.Models;
using Microsoft.Data.SqlClient;
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
            ExecuteScriptForQueryDataViaSqlQuery(db);
            //ExecuteStoredProcedureForQueryDataViaSqlQueryRaw(db);
            //ExecuteStoredProcedureForInsertData(db);
        }

        private static void ExecuteStoredProcedureForInsertData(AdventureWorksLT2022Context db)
        {
            // Insert, Delete, Update，總之就不是要回傳資料的....等等等。
            Console.WriteLine("ExecuteStoredProcedureForInsertData");

            // 參數化方式
            string sql = $"insert [SalesLT].Product ([Name],[ProductNumber],[StandardCost],[ListPrice],[Size],[Weight],[ProductCategoryID],[ProductModelID],[SellStartDate],[SellEndDate],[DiscontinuedDate],[ThumbNailPhoto]" +
                $",[ThumbnailPhotoFileName],[rowguid],[ModifiedDate]) VALUES( @Name, @ProductNumber)";
            var sqlParameters = new object[] {
                  new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
                new SqlParameter("@Name", ""),
            };
            int affectedRows = db.Database.ExecuteSqlRaw(sql, sqlParameters);
            Console.WriteLine($"影響行數 = {affectedRows}");

            // Hard Code 執行SQL語法
            string name = "FR-R92B-58產品3";
            string sql2 = $"insert [SalesLT].Product ([Name],[ProductNumber],[StandardCost],[ListPrice],[Size],[Weight],[ProductCategoryID],[ProductModelID],[SellStartDate],[SellEndDate],[DiscontinuedDate],[ThumbNailPhoto]" +
                $",[ThumbnailPhotoFileName],[rowguid],[ModifiedDate])" +
                $"select N'" + name + "',N'FR-R92B-58產品3',N'Black',1059.31,1431.50,N'58',1016.04,18,6,'2002-06-01 00:00:00.000'," +
                $"NULL,NULL,0x47494638396150003100F70000000000800000008000808000000080800080008080808080C0C0C0FF000000FF00FFFF000000FFFF00FF00FFFFF" +
                $"FFFFF000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                $"0000000000000000000000000330000660000990000CC0000FF0033000033330033660033990033CC0033FF0066000066330066660066990066CC0066FF0099000099330099" +
                $"660099990099CC0099FF00CC0000CC3300CC6600CC9900CCCC00CCFF00FF0000FF3300FF6600FF9900FFCC00FFFF3300003300333300663300993300CC3300FF333300333333" +
                $"3333663333993333CC3333FF3366003366333366663366993366CC3366FF3399003399333399663399993399CC3399FF33CC0033CC3333CC6633CC9933CCCC33CCFF33FF003" +
                $"3FF3333FF6633FF9933FFCC33FFFF6600006600336600666600996600CC6600FF6633006633336633666633996633CC6633FF6666006666336666666666996666CC6666FF669" +
                $"9006699336699666699996699CC6699FF66CC0066CC3366CC6666CC9966CCCC66CCFF66FF0066FF3366FF6666FF9966FFCC66FFFF9900009900339900669900999900CC9900FF" +
                $"9933009933339933669933999933CC9933FF9966009966339966669966999966CC9966FF9999009999339999669999999999CC9999FF99CC0099CC3399CC6699CC9999CCCC99CC" +
                $"FF99FF0099FF3399FF6699FF9999FFCC99FFFFCC0000CC0033CC0066CC0099CC00CCCC00FFCC3300CC3333CC3366CC3399CC33CCCC33FFCC6600CC6633CC6666CC6699CC66CCCC" +
                $"66FFCC9900CC9933CC9966CC9999CC99CCCC99FFCCCC00CCCC33CCCC66CCCC99CCCCCCCCCCFFCCFF00CCFF33CCFF66CCFF99CCFFCCCCFFFFFF0000FF0033FF0066FF0099FF00CC" +
                $"FF00FFFF3300FF3333FF3366FF3399FF33CCFF33FFFF6600FF6633FF6666FF6699FF66CCFF66FFFF9900FF9933FF9966FF9999FF99CCFF99FFFFCC00FFCC33FFCC66FFCC99FFCC" +
                $"CCFFCCFFFFFF00FFFF33FFFF66FFFF99FFFFCCFFFFFF21F90401000010002C00000000500031000008FF00FF091C48B0A0C18308132A5CC8B0A1C38710234A9C48B1A2" +
                $"C58B18336ADCC8B1A3C78F20438A1C49B2A4C9932853AA5C9911058A812E17C664F9D0E5CB7F3313E6A4C9D0A6C099366FC27C19D367CEA04371DE44CA1169D1A54295" +
                $"2A25FA1467D2A7547F46C558542AD0A855A58A9D9AD52BD4A654C3EE547BD6AC58AC3E377E2DBB96EED9AF6FCB7A9C9B97205BAD7DB1F6952B74EED6BF79E3B2DDC9" +
                $"5362E1AD8D2F328D4CB9B2E5CB9831C77518D62063C6223743040DD82F6490558FA6BD0B152F5EB2A8B3AA7EEC96B651D96D3BFE9D8D7BE86EBB1F7F034ECD" +
                $"3AF06AD292831237EEB6F9F290AAB5B65DEE9A7A6EC249A7FA7EAEDC79EBD399C38B121F4FBEBCF9F3E8D3AB5FCFBEBDFBF7EF0302003B,N'no_image_available_small.gif',NEWID()," +
                $"'2008-03-11 10:01:36.827';";
            int affectedRows2 = db.Database.ExecuteSqlRaw(sql);
            Console.WriteLine($"影響行數 = {affectedRows2}");
        }

        private static void ExecuteScriptForQueryDataViaSqlQuery(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("ExecuteScriptForQueryDataViaSqlQuery");
            string productName = "Mountain-500";
            FormattableString formattableString =
                @$"SELECT
                    P.Name as [ProductName],
                    PC.Name as [CategoryName]
                    FROM SalesLT.Product P WITH(NOLOCK)
                   INNER JOIN SalesLT.ProductCategory PC WITH(NOLOCK) ON P.ProductCategoryID = PC.ProductCategoryID
                   WHERE P.Name LIKE '%' + {productName} + '%'";
            List<ProductData> productDatas = db.Database.SqlQuery<ProductData>(formattableString).ToList();
            foreach (var item in productDatas)
            {
                Console.WriteLine($"Category = {item.CategoryName}, Product = {item.ProductName}");
            }

        }

        private static void ExecuteStoredProcedureForQueryDataViaSqlQuery(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("ExecuteStoredProcedureForQueryDataViaSqlQuery");
            // exec Usp_GetProductByProductName 'Mountain-500'
            string productName = "Mountain-500";
            FormattableString formattableString = $"EXEC [dbo].Usp_GetProductByProductName {productName}, {productName}";
            List<ProductData> productDatas = db.Database.SqlQuery<ProductData>(formattableString).ToList();
            //$"{spName} @ProductName", new SqlParameter("@ProductName", productName)            

            foreach (var item in productDatas)
            {
                Console.WriteLine($"Category = {item.CategoryName}, Product = {item.ProductName}");
            }

        }
        private static void ExecuteStoredProcedureForQueryDataViaSqlQueryRaw(AdventureWorksLT2022Context db)
        {
            Console.WriteLine("ExecuteStoredProcedureForQueryDataViaSqlQueryRaw");
            // exec Usp_GetProductByProductName 'Mountain-500'
            string spName = "[dbo].Usp_GetProductByProductName";
            string productName = "Mountain-500";
            List<ProductData> productDatas =
                db.Database.SqlQueryRaw<ProductData>($"{spName} @ProductName",
                                                new SqlParameter("@ProductName", productName)
            ).ToList();

            foreach (var item in productDatas)
            {
                Console.WriteLine($"Category = {item.CategoryName}, Product = {item.ProductName}");
            }

        }
        public class ProductData
        {
            public string? ProductName { get; set; }

            public string? CategoryName { get; set; }
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
