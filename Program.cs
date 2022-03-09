using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
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
                        configuration.GetSection(nameof(AuthenticationOptions)).Bind(settings);
                    });
                    services.AddScoped<IQueryService, QueryService>();
                })
                .Build();
                

            host.Run();
        }
    }
}