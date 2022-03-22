using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Solution.RuralWater.AZF.Options;
using Solution.RuralWater.AZF.Services;
using Solution.RuralWater.AZF.Helpers;
using Microsoft.Extensions.Options;

namespace Solution.RuralWater.AZF
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddOptions<Secrets>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection(nameof(Secrets)).Bind(settings);
                    }).Validate(config =>
                    {
                        if (string.IsNullOrEmpty(config.Password) || string.IsNullOrEmpty(config.ApiKey))
                            return false;

                        return true;
                    }, "Secrets configuration must not have any null or empty values.");
                    services.AddOptions<AuthenticationOptions>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("AuthOptions").Bind(settings);
                    }).Validate(config =>
                    {
                        var dictionary = QueryParamHelpers.ToDictionary<string>(config);
                        foreach(var item in dictionary){
                            if(string.IsNullOrEmpty(item.Value)){
                                return false;
                            }
                        }
                        return true;
                    }, "Authentication Options configuration must not have any null or empty values.");
                    services.AddScoped<IQueryService, QueryService>();
                    services.AddSingleton<AuthenticationHelper>();
                    services.AddSingleton<AuthorizationHelper>();
                })
                .Build();


            host.Run();
        }
    }
}