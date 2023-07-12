using BudgetCalculator.Bots.Telegram;

using Microsoft.EntityFrameworkCore.Metadata;

using System.Runtime.InteropServices;

using Telegram.Bot.Types;

namespace BudgetCalculator.Data.Services
{
	public class TelegramService : IHostedService, IDisposable
	{
		
		private readonly ILogger<TelegramService> _logger;
		private Timer? _timer = null;
		private readonly IConfiguration _configuration;
		readonly TelegramBot bot;



		public TelegramService(ILogger<TelegramService> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
			bot = new TelegramBot(_configuration, logger);
			bot.InitializeTelegramBot();
		}



		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Hosted Service running.");




			_timer = new Timer(DoWork, null, TimeSpan.Zero,
				TimeSpan.FromSeconds(300));

			return Task.CompletedTask;




		}

		private void DoWork(object? state)
		{
			_logger.LogInformation($"Monitoring...");

		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Hosted Service is stopping.");

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}

		public async Task sendMessage(ChatId id, string message)
		{

			if (bot.ChannelID is not null)
			{
				await bot.sendMessageAsync(bot.ChannelID, message);
			}
			else
			{
				id = 6166907512;
				await bot.sendMessageAsync(id, message);
			}



		}





	}
}
