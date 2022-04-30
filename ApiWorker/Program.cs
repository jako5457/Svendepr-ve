// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using TopicFramework;
using TopicFramework.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiDataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

IConfiguration config = new ConfigurationBuilder()
                        .AddEnvironmentVariables()
                        .AddJsonFile("appsettings.json")
                        .AddUserSecrets<Program>()
                        .Build();

HostBuilder hostBuilder = new HostBuilder();
hostBuilder.ConfigureLogging((context,builder) => builder.AddConsole());

TfRabbitmqService.MessageDebug = true;
TfRabbitmqService.MqttMode = true;

hostBuilder.ConfigureServices(services =>
{
    //services.AddDbContext<ApiDbcontext>(b => b.UseSqlServer(config.GetConnectionString("LocalConnection")));
    services.AddDbContext<ApiDbcontext>(b => b.UseSqlServer(config.GetConnectionString("ApiDatabase")));
    services.AddTfRabbitConnectionFactory(factory =>
    {
        factory.HostName = config.GetValue<string>("RabbitMq:Host");
        factory.UserName = config.GetValue<string>("RabbitMq:User");
        factory.Password = config.GetValue<string>("RabbitMq:Password");
        factory.Port = config.GetValue<int>("RabbitMq:Port");
    });
    services.AddTopicFrameWork(Assembly.GetEntryAssembly());
    services.AddTfRabbit();
});

var app = hostBuilder.Build();

app.Run();






