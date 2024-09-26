using DemoWorkerService;

// 在跨平台，背景執行的語法。
//var builder = Host.CreateApplicationBuilder(args); // 如果要使用Windows 服務，則不能使用範本預設的 CreateApplicationBuilder
//builder.ser.AddHostedService<Worker>();

// 在 Windows 平台上，註冊成 Windows Service 的寫法。
var builder = Host.CreateDefaultBuilder(args)
                  .UseWindowsService()
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddHostedService<Worker>();
                  });

var host = builder.Build();
host.Run();
