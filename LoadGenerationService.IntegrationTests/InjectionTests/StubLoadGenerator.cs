using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadGeneratorService;
using LoadGeneratorService.LoadGenerator;

namespace LoadGenerationService.IntegrationTests.InjectionTests
{
    public class StubLoad : ILoad
    {
        public Task<IList<int>> ExecuteLoad(int upTo, bool validate)
        {
            IList<int> elem = Enumerable.Range(0, upTo).ToList();
            return Task.FromResult(elem);
        }
    }
}
