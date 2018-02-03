using LoadGeneratorService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoadGenerationService.IntegrationTests.InjectionTests
{
    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IInternalLoadGenerator, StubLoadGenerator>();
        }
    }
}
