using BudgetCalculator.Bots.Telegram;

using Microsoft.EntityFrameworkCore.Metadata;

using System.Runtime.InteropServices;

using Telegram.Bot.Types;

namespace BudgetCalculator.Data.Services
{
	public class TelegramService: IHostedService, IDisposable
	{
		private int executionCount = 0;
		private readonly ILogger<TelegramService> _logger;
		private Timer? _timer = null;
		private readonly IConfiguration _configuration;
		TelegramBot bot;



		public TelegramService(ILogger<TelegramService> logger,IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
			bot = new TelegramBot(_configuration,logger);
			bot.InitializeTelegramBot();
		}

	

		public  Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Hosted Service running.");

			 


			_timer = new Timer(DoWork, null, TimeSpan.Zero,
				TimeSpan.FromSeconds(5));

			return   Task.CompletedTask;




		}

		private void DoWork(object? state)
		{
			//var count = Interlocked.Increment(ref executionCount);

			//_logger.LogInformation(
			//	"Timed Hosted Service is working. Count: {Count}", count);
			//bot.InitializeTelegramBot();

		}

		public Task StopAsync(CancellationToken stoppingToken)
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
			
			if(bot.ChannelID is not null)
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
