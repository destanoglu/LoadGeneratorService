using System.Threading.Tasks;
using FluentAssertions;
using LoadGeneratorService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace LoadGenerationService.IntegrationTests.InjectionTests
{
    [TestFixture]
    public class InjectionTest
    {
        [Test]
        public async Task TestMethod1()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartUp>();

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                string result = await client.GetStringAsync("/api/load/8");
                result.Should().Be("0, 1, 2, 3, 4, 5, 6, 7");
            }
        }
    }
}