namespace CookieMonster
{
    using Tweetinvi;
    using Tweetinvi.Core.Credentials;

    class TweetService
    {
        public TweetService(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            Auth.SetCredentials(new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret));
        }

        public void Publish(string message)
        {
            Tweet.PublishTweet(message);
        }

    }
}
