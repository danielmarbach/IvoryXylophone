namespace PepperPig
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Anything;
    using Anything.Contracts;
    using ColoredConsole;
    using NServiceBus;
    using Tweetinvi;
    using Tweetinvi.Core.Credentials;

    class Piggy
    {
        public static async Task StartAsync(
            IEndpointInstance endpoint,
            string track,
            string consumerKey,
            string consumerSecret,
            string accessToken,
            string accessTokenSecret,
            string endpointName)
        {
            var credentials = new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            while (true)
            {
                try
                {
                    var stream = Stream.CreateFilteredStream(credentials);
                    stream.AddTrack(track);

                    var sessionId = Guid.NewGuid();
                    stream.StreamStarted += (sender, args) =>
                    {
                        sessionId = Guid.NewGuid();
                        ColorConsole.WriteLine(
                            $"{DateTime.UtcNow.ToLocalTime()}".DarkGray(),
                            " ",
                            $" {track} ".DarkCyan().OnWhite(),
                            " ",
                            "stream started with session ID".Gray(),
                            " ",
                            $"{sessionId}".White());
                    };

                    stream.StreamStopped += (sender, args) => ColorConsole.WriteLine(
                        $"{DateTime.UtcNow.ToLocalTime()} ".DarkGray(),
                        $" {track} ".DarkCyan().OnWhite(),
                        " stream stopped.".Red(),
                        args.Exception == null ? string.Empty : $" {args.Exception.Message}".DarkRed());

                    stream.MatchingTweetReceived += (sender, e) =>
                    {
                        var userName = track.Replace('#', '@');
                        var command = new StartSaga
                        {
                            MessageId = Guid.NewGuid(),
                            Text = e.Tweet.Text
                                .Replace(track, null)
                                .Replace(userName, null).Trim(),
                        };

                        ColorConsole.WriteLine(
                            $"Starting saga".Gray(),
                            " ",
                            $"{command.MessageId}".Cyan(),
                            " ",
                            $"to say:".Gray(),
                            " ",
                            $"{command.Text}".White());

                        endpoint.CreateBusContext().Send(command).Wait();
                    };

                    await stream.StartStreamMatchingAnyConditionAsync();
                }
                catch (Exception ex)
                {
                    ColorConsole.WriteLine($"{DateTime.UtcNow.ToLocalTime()} ".DarkGray(), "Error listening to Twitter stream.".Red(), $" {ex.Message}".DarkRed());
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
