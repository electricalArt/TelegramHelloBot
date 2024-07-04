using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.Enums;
using StringMath;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7331498806:AAHiZ1jerTgcHA6iACpsVZdYyND9gvqhRsg", cancellationToken: cts.Token);
bot.StartReceiving(HandleUpdate, async (bot, ex, ct) => Console.Error.WriteLine(ex));

var me = await bot.GetMeAsync();
Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot

// method that handle updates coming for the bot:
async Task HandleUpdate(ITelegramBotClient bot, Update update, CancellationToken ct)
{
    if (update.Message is null) return;			// we want only updates about new Message
    if (update.Message.Text is null) return;	// we want only updates about new Text Message
    var msg = update.Message;
    Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");
    // let's echo back received text in the chat
    string answer = "";

    try
    {
        if (msg.Text.StartsWith("/gettime")) {
            Console.WriteLine("Luck!");
            answer = DateTime.Now.ToString();
        }
        if (msg.Text.StartsWith("/resolvemath")) {
            answer = msg.Text.Substring(12).Eval().ToString();
        }
    }
    catch
    {
        answer = "Incorrect input";
    }

    await bot.SendTextMessageAsync(msg.Chat, answer, cancellationToken: cts.Token);
}

/*
async Task OnInlineQueryReceived(ITelegramBotClient bot, InlineQuery inlineQuery)
{
    await bot.AnswerInlineQueryAsync(inlineQuery.Id, )
}

*/