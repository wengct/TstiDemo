using DemoWorkerService;

// �b�󥭥x�A�I�����檺�y�k�C
//var builder = Host.CreateApplicationBuilder(args); // �p�G�n�ϥ�Windows �A�ȡA�h����ϥνd���w�]�� CreateApplicationBuilder
//builder.ser.AddHostedService<Worker>();

// �b Windows ���x�W�A���U�� Windows Service ���g�k�C
var builder = Host.CreateDefaultBuilder(args)
                  .UseWindowsService()
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddHostedService<Worker>();
                  });

var host = builder.Build();
host.Run();
