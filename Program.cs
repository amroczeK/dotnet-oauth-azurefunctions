using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Solution.RuralWater.AZF.Options;
using Solution.RuralWater.AZF.Services;

namespace Solution.RuralWater.AZF
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services => {
                    services.AddOptions<Secrets>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection(nameof(Secrets)).Bind(settings);
                    });
                    services.AddOptions<AuthenticationOptions>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("AuthOptions").Bind(settings);
                    });
                    services.AddScoped<IQueryService, QueryService>();
                })
                .Build();
                

            host.Run();
        }
    }
}