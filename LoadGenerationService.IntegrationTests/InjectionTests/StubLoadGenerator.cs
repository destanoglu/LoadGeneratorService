using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadGeneratorService;

namespace LoadGenerationService.IntegrationTests.InjectionTests
{
    public class StubLoadGenerator : IInternalLoadGenerator
    {
        public Task<IList<int>> GenerateLoad(int upTo, bool validate)
        {
            IList<int> elem = Enumerable.Range(0, upTo).ToList();
            return Task.FromResult(elem);
        }
    }
}
