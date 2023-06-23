using BudgetCalculator.Data.Services;

using SQLitePCL;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace BudgetCalculator.Bots.Telegram
{
	public class TelegramBot
	{
		private readonly IConfiguration _configuration;
		TelegramBotClient botClient;
		private readonly ILogger<TelegramService> _logger;
		

		public TelegramBot(IConfiguration configuration, ILogger<TelegramService> logger)
		{
			_logger = logger;
			_configuration = configuration;
			botClient = new TelegramBotClient(_configuration.GetValue<string>("Telegram:Token"));


		}
		public async Task InitializeTelegramBot()
		{

			using CancellationTokenSource cts = new();
			ReceiverOptions receiverOptions = new()
			{
				AllowedUpdates = Array.Empty<UpdateType>()
			};

			botClient.StartReceiving(
				updateHandler: HandleUpdateAsync,
				pollingErrorHandler: HandlePollingErrorAsync,
				receiverOptions: receiverOptions,
				cancellationToken: cts.Token);

			var me = await botClient.GetMeAsync();
			//Console.WriteLine($"Start listening for @{me.Username}");
			_logger.LogInformation($"Start listening for @{me.Username}");

		}

		public async Task sendMessageAsync(ChatId chatID, string message)
		{
			chatID = 6166907512;
			await botClient.SendTextMessageAsync(chatID, message);
		}



		public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (update.Message is not { } message)
			{
				return;
			}
			if (message.Text is not { } messageText)
			{
				return;
			}

			var chatId = message.Chat.Id;

			//Console.WriteLine($"Received a '{messageText}' message in chat {chatId}");
			_logger.LogInformation($"Received a '{messageText}' message in chat {chatId}");

			Message sentMessage = await botClient.SendTextMessageAsync(chatId,
				text: "You are welcome to Budget Calculator",
				cancellationToken: cancellationToken);

		}

		Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			var ErrorMessage = exception switch
			{
				ApiRequestException apiRequestException
				=> $"Telegram API ERROR :\n[{apiRequestException.ErrorCode}]\n {apiRequestException.Message}",
				_ => exception.ToString()
			};
			Console.WriteLine(ErrorMessage);
			return Task.CompletedTask;


		}
	}
}
