using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Webhook.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly ChannelWriter<Update> _updatesQueue;

        public UpdateController(Channel<Update> updatesQueue)
        {
            _updatesQueue = updatesQueue.Writer;
        }

        // POST api/update
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            Console.WriteLine($"[{DateTime.Now}] {update?.Id} Enqueue...");
            await _updatesQueue.WriteAsync(update);
            return Ok();
        }
    }
}
