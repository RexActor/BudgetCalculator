using BudgetCalculator.Data.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using SQLitePCL;

using System;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BudgetCalculator.Bots.Telegram
{
	public class TelegramBot
	{
		private readonly IConfiguration _configuration;
		TelegramBotClient botClient;
		private readonly ILogger<TelegramService> _logger;
		public ChatId ChannelID { get; set; }
		

		


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
			
			_logger.LogInformation($"Start listening for @{me.Username}");

		}

		public async Task sendMessageAsync(ChatId chatID, string message)
		{
			
			await botClient.SendTextMessageAsync(chatID, message);

		}


		



		public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{

			#region THIS DON@T WORK BECAUSE ALWAYS ONE WILL BE "TRUE" AND WILL EXIT FUNCTION

			//if(update.ChannelPost is not { } channelPost || update.Message is not { } message)
			//{
			//	return;
			//}
			//if(channelPost.Text is not { } channelPostText || message.Text is not { } messageText)
			//{
			//	return;
			//}

			#endregion


			#region DIFFERENT APPROACH
			///CHECK IF UPDATE IS NULL, IF IS NULL - RETURN
			///IF UPDATE IS NOT NULL CONTINUE TO CHECK IF UPDATE IS FROM CHANNEL POST
			///IF UPDATE IS FROM CHANNEL POST IT ENTERS IF STATEMENT AND DISPLAY RECEIVED MESSAGE IN LOGGER
			///
			/// IF UPDATE IS NOT FROM CHANNEL IT CHECKS IF UPDATE IS DIRECT MESSAGE 
			/// IF UPDATE IS DIRECT MESSAGE IT ENTERS IF STATEMENT AND DISPLAYS RECEIVED MESSAGE IN LOGGER



			if (update is null) { return; }

			
			

			var channelPost = update.ChannelPost;
			if (channelPost is not null)
			{
		
				
				var channelPostText = channelPost.Text;
				var chatId = channelPost.Chat.Id;
				var channelName = channelPost.Chat.Title;
				this.ChannelID = chatId;
				_logger.LogInformation($"Received a '{channelPostText}' message in {channelName} with ID {chatId}");
			
				
				
				Message sentMessage = await botClient.SendTextMessageAsync(chatId,
					text: $"AUTHOR! Yeey. I'm a LIVE in CHANNEL {channelName}",
					cancellationToken: cancellationToken);
			}



			var message = update.Message;

			if (message is not null)
			{
				var messageText = message.Text;
				var chatId = message.Chat.Id;
				
				var replyMessage = string.Empty;

				_logger.LogInformation($"Received a '{messageText}' message in chat {chatId}");

				if (messageText == "/group")
				{

					var keyboard = new InlineKeyboardMarkup(
						InlineKeyboardButton.WithUrl("Join!!!", "https://t.me/+abvyOpD-Rsc4ODk0"));
					Message sentMessage = await botClient.SendTextMessageAsync(chatId, "Please Join private chat for updates\n", replyMarkup: keyboard);
					
					
				}
				else
				{
					replyMessage = $"{message.From?.Username:'NO_USERNAME'}! You are welcome to Budget Calculator. I'm here to work as notification pusher. You will receive any required updates in your Telegram application on phone or desktop computer";

					Message sentMessage = await botClient.SendTextMessageAsync(chatId,
						text: replyMessage,
						cancellationToken: cancellationToken);
				}

				

			}


			#endregion









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
