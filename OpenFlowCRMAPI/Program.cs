
using OpenFlowCRMAPI;
using Serilog;

Host.CreateDefaultBuilder(args)
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/OpenFlowCRMAPI.log", rollingInterval: RollingInterval.Day);
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.ConfigureAppConfiguration((context, config) =>
        {
            config.SetBasePath(context.HostingEnvironment.ContentRootPath);
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        });
        webBuilder.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(5001, listenOptions =>
            {
				var certificatePassword = Environment.GetEnvironmentVariable("CERTIFICATE_PASSWORD");
				listenOptions.UseHttps("ssl\\certificate.pfx", certificatePassword);
            });
        });
    }).Build().Run();
