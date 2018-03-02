using System.Threading;
using System.Threading.Tasks;

namespace LoadGeneratorService.LoadGenerator
{
    public interface IBackgroundLoadGenerator : IExecutable
    {
        int SleepInterval { get; set; }
    }

    public class BacgroundLoadGenerator : IBackgroundLoadGenerator
    {
        private bool _executing = false;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private IBackgroundLoadExecutor _backgroundLoadExecutor;

        public BacgroundLoadGenerator(IBackgroundLoadExecutor backgroundLoadExecutor)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _backgroundLoadExecutor = backgroundLoadExecutor;
        }

        public int SleepInterval { get; set; }


        public void Start()
        {
            if (_executing)
            {
                return;
            }
            
            _executing = true;

            Task.Run(() =>
            {
                while (_executing)
                {
                    Thread.Sleep(SleepInterval);
                    _backgroundLoadExecutor.BlockingCollection.Add(5000);
                }
            }, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _executing = false;
            _cancellationTokenSource.Cancel();
        }
    }
}
