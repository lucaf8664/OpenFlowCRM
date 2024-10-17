using Serilog;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

Host.CreateDefaultBuilder(args)
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/OpenFlowCRMApp.log", rollingInterval: RollingInterval.Day);
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<OpenFlowCRMApp.Startup>();
        webBuilder.ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(context.HostingEnvironment.ContentRootPath);
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        });
        webBuilder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });
        webBuilder.UseUrls("https://*:443"); // Add this line to use HTTPS
        webBuilder.UseKestrel(serverOptions =>
        {
            serverOptions.ConfigureHttpsDefaults(co =>
            {
                var certPath = builder.Configuration["Certificate:Path"];
                var certificatePassword = builder.Configuration["Certificate:Password"];
                co.ServerCertificate = new X509Certificate2(certPath, certificatePassword);
            });
        });
    }).Build().Run();
