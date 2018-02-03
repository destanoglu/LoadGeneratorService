using System.Threading.Tasks;
using FluentAssertions;
using LoadGeneratorService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace LoadGenerationService.IntegrationTests
{
    [TestFixture]
    public class DefaultTest
    {
        [Test]
        public async Task TestMethod1()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                string result = await client.GetStringAsync("/api/load/8");
                result.Should().Be("2, 3, 5, 7");
            }
        }
    }
}
