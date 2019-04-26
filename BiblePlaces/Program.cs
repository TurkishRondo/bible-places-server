using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BiblePlaces
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            // To expose the webserver to the network, implement like this.
            // https://weblog.west-wind.com/posts/2016/sep/28/external-network-access-to-kestrel-and-iis-express-in-aspnet-core

            // IMPORTANT!!!
            // This is the URL:port of the web server.  Be sure that the URL matches the one specified in the android
            // project.  If you're running the webserver on a Windows PC on your local area network:
            // 1. Find it's IPv4 address on the network by opening a command prompt and typing "ipconfig".
            // 2. You may also have to create an "Outbound Rule" for your firewall.  To do so, navigate to Windows Security ->
            //    Firewall -> Advanced Settings, then create an Outbound Rule with a TCP protocol on port 5001.
            var hostUrl = "http://192.168.1.252:5001";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(hostUrl) // <--- This is key.
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .Build();

            host.Run();
        }
    }
}
