using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Linq2TwitterTest
{
    // https://linqtotwitter.codeplex.com/wikipage?title=Single%20User%20Authorization
    // https://github.com/JoeMayo/LinqToTwitter/wiki/LINQ-to-Twitter-FAQ
    // https://linqtotwitter.codeplex.com/wikipage?title=Error%20Handlin

    // what to do with Rate limit exceeded: LinqToTwitter.TwitterQueryException - 
    // A few examples are 88/Rate Limit Exceeded, meaning that you've sent more requests during a 15 minute window than what you're allowed; 
    // 187/Status is a duplicate, meaning that you tweeted the exact same text twice; 
    // or 32/Could not authenticate you, meaning that Twitter wasn't able to verify who you are.

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GetTweets();
                Thread.Sleep(1000);
            }
        }

        private async static void GetTweets()
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = @"51WXplzDr3NHpU2aJlA8f8rmU",
                    ConsumerSecret = @"UpT72NjZiWYXd9whjDES18Wc7dj4ngSEGv9yYn5Cg9UcnaoVm2",
                    AccessToken = @"801059003791003648-aojSHBewwNwx4thqYnufEgj374q11mQ",
                    AccessTokenSecret = @"sFrfW9OhxsWuSvZjsvahNVUBRVg1hGnhpMllpAQ8TSmea"
                }
            };

            var twitterCtx = new TwitterContext(auth);

            var searchResponse =
                await
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                       search.Query == "#andriitest2"
                 select search)
                .SingleOrDefaultAsync();

            if (searchResponse != null && searchResponse.Statuses != null)
                searchResponse.Statuses.ForEach(tweet =>
                    Console.WriteLine(
                        "User: {0}, Tweet: {1}",
                        tweet.User.ScreenNameResponse,
                        tweet.Text));
        }
    }
}
