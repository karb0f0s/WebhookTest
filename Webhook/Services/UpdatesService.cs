using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Types;

namespace Webhook.Services
{
    public class UpdatesService : BackgroundService
    {
        private readonly ChannelReader<Update> _updatesQueue;

        public UpdatesService(Channel<Update> updatesQueue)
        {
            _updatesQueue = updatesQueue.Reader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var update in _updatesQueue.ReadAllAsync(stoppingToken))
            {
                //Task.Run(async () =>  {
                    Console.WriteLine($"[{DateTime.Now}] {update?.Id} Start processing...");
                    await Task.Delay(10_000);
                    Console.WriteLine($"[{DateTime.Now}] {update?.Id} Stop processing...");
                //});
            }
        }
    }
}
