using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoadGeneratorService
{
    public interface IInternalLoadGenerator
    {
        Task<IList<int>> GenerateLoad(int upTo, bool validate);
    }

    public class ValidationResponse
    {
        public ValidationResponse(bool validationResponse)
        {
            IsValid = validationResponse;
        }
        public bool IsValid { get; set; }
    }

    public class PrimeFinder : IInternalLoadGenerator
    {
        public async Task<IList<int>> GenerateLoad(int upTo, bool validate)
        {
            var primes = new List<int>();
            int p;
            for (int i = 2; i < upTo; i++)
            {
                p = 0;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                        p = 1;
                }
                if (p == 0)
                {
                    if (validate)
                    {
                        var validationResult = await Validate(i);
                        if (!validationResult)
                        {
                            continue;
                        }
                    }
                    primes.Add(i);
                }
            }

            return primes;
        }

        private async Task<bool> Validate(int value)
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync("http://127.0.0.1:5001/api/validation/" + value.ToString()))
            {
                var data = await response.Content.ReadAsStringAsync();
                var validationResponse = JsonConvert.DeserializeObject<ValidationResponse>(data);
                return validationResponse.IsValid;
            }
        }
    }
}
