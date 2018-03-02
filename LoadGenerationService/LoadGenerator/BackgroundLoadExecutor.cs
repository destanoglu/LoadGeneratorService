using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace LoadGeneratorService.LoadGenerator
{
    public interface IBackgroundLoadExecutor : IExecutable
    {
        BlockingCollection<int> BlockingCollection { get; }
    }

    public class BackgroundLoadExecutor : IBackgroundLoadExecutor
    {
        private readonly ILoad _load;
        private bool _executing = false;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public BlockingCollection<int> BlockingCollection { get; set; }

        public BackgroundLoadExecutor(ILoad load)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            BlockingCollection = new BlockingCollection<int>();
            _load = load;
        }

        public void Start()
        {
            if (_executing)
            {
                return;
            }

            _executing = true;

            Task.Run(() =>
            {
                try
                {
                    foreach (var item in BlockingCollection.GetConsumingEnumerable())
                    {
                        _load.ExecuteLoad(item, false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Exiting the background load executor thread");
            }, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _executing = false;
            _cancellationTokenSource.Cancel();
        }
    }
}
