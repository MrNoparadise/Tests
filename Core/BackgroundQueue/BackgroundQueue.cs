using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.BackgroundQueue
{
    public class BackgroundQueue
    {
        private readonly ILogger<BackgroundQueue> _logger;
        public readonly int MillisecondsToWaitBeforePickingUpTask = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
        public int MaxConcurrentCount = 10;
        private readonly IServiceProvider _service;
        public int ConcurrentCount;

        public readonly ConcurrentQueue<Func<CancellationToken, IServiceProvider, Task>> TaskQueue = new ConcurrentQueue<Func<CancellationToken, IServiceProvider, Task>>();

        public BackgroundQueue(IServiceProvider service)
        {
            _service = service;
            using (var scope = service.CreateScope())
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<BackgroundQueue>>();
            }
        }

        public void Enqueue(Func<CancellationToken, IServiceProvider, Task> task)
        {
            TaskQueue.Enqueue(task);
        }

        public async Task Dequeue(CancellationToken cancellationToken)
        {
            if (TaskQueue.TryDequeue(out var nextTaskAction))
            {
                Interlocked.Increment(ref ConcurrentCount);
                try
                {
                    await nextTaskAction(cancellationToken, _service);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Возникла ошибка {e.Message} в {DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")}");
                }
                finally
                {
                    Interlocked.Decrement(ref ConcurrentCount);
                }
            }

            await Task.CompletedTask;
        }
    }
}
