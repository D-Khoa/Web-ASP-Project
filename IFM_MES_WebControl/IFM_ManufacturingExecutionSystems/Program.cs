using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace IFM_ManufacturingExecutionSystems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var config = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddEnvironmentVariables()
            //.AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
            //.AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            //.Build();

            //var certificateSettings = config.GetSection("certificateSettings");
            //string certificateFileName = certificateSettings.GetValue<string>("filename");
            //string certificatePassword = certificateSettings.GetValue<string>("password");

            //var certificate = new X509Certificate2(certificateFileName, certificatePassword);

            //var host = new WebHostBuilder()
            //    .UseKestrel(
            //        options =>
            //        {
            //            options.AddServerHeader = false;
            //            options.Listen(IPAddress.None, 443, listenOptions =>
            //            {
            //                listenOptions.UseHttps(certificate);
            //            });
            //        }
            //    )
            //    .UseConfiguration(config)
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseStartup<Startup>()
            //    //.UseUrls("https://localhost:44321")
            //    .Build();

            //host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddEnvironmentVariables()
                        .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                        .Build();
                    var certificateSettings = config.GetSection("certificateSettings");
                    string certificateFileName = certificateSettings.GetValue<string>("filename");
                    string certificatePassword = certificateSettings.GetValue<string>("password");
                    var certificate = new X509Certificate2(certificateFileName, certificatePassword);
                    webBuilder.UseKestrel(
                        options =>
                        {
                            options.AddServerHeader = false;
                            options.Listen(IPAddress.Any, 443, listenOptions =>
                            {
                                listenOptions.UseHttps(certificate);
                            });
                        });
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.ConfigureKestrel(o =>
                    //{
                    //    o.ConfigureHttpsDefaults(o =>
                    //    o.ClientCertificateMode =
                    //    ClientCertificateMode.RequireCertificate);
                    //});
                });
    }
}
