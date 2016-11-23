using System;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Events;
using Tweetinvi.Models;

// https://apps.twitter.com/app/13131014
// https://github.com/linvi/tweetinvi/wiki
// why does it change url to something unreadable
public class Program
{
    //public static void Main(string[] args)
    //{
    //    var consumerKey = @"51WXplzDr3NHpU2aJlA8f8rmU";
    //    var consumerSecret = @"UpT72NjZiWYXd9whjDES18Wc7dj4ngSEGv9yYn5Cg9UcnaoVm2";
    //    var accessToken = @"801059003791003648-aojSHBewwNwx4thqYnufEgj374q11mQ";
    //    var accessTokenSecret = "sFrfW9OhxsWuSvZjsvahNVUBRVg1hGnhpMllpAQ8TSmea";

    //    Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
    //    var user = User.GetAuthenticatedUser();

    //    Console.WriteLine(user);
    //    Console.ReadLine();
    //}

    public static void Main(string[] args)
    {
        //if (args.Length == 0)
        //{
        //    Console.WriteLine("You didn't specified tracks to stream from Twitter!");
        //    return;
        //}
        //var tracks = args[0].Split(',').ToArray();
        //var consumerKey = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_KEY") ?? args.Skip(1).FirstOrDefault();
        //var consumerSecret = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_SECRET") ?? args.Skip(2).FirstOrDefault();
        //var accessToken = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN") ?? args.Skip(3).FirstOrDefault();
        //var accessTokenSecret = Environment.GetEnvironmentVariable("TWITTER_ACCESS_SECRET") ?? args.Skip(4).FirstOrDefault();

        var tracks = "#andriitest2".Split(',').ToArray();

        var consumerKey = @"51WXplzDr3NHpU2aJlA8f8rmU";
        var consumerSecret = @"UpT72NjZiWYXd9whjDES18Wc7dj4ngSEGv9yYn5Cg9UcnaoVm2";
        var accessToken = @"801059003791003648-aojSHBewwNwx4thqYnufEgj374q11mQ";
        var accessTokenSecret = @"sFrfW9OhxsWuSvZjsvahNVUBRVg1hGnhpMllpAQ8TSmea";

        var credentials = new TwitterCredentials(
            consumerKey,
            consumerSecret,
            accessToken,
            accessTokenSecret
        );

        var stream = Stream.CreateFilteredStream(credentials);
        tracks.ForEach(track => stream.AddTrack(track));
        stream.MatchingTweetReceived += (sender, arg) =>
            {
                try
                {
                    Console.WriteLine(arg.Tweet.Text);
                    Console.WriteLine(arg.Tweet.CreatedBy);
                    Console.WriteLine(arg.Tweet.Urls[0].ExpandedURL);
                }
                catch
                {
                    // if we fail here, it will be the end :)
                }
            };
        stream.StartStreamMatchingAnyCondition();

        Console.WriteLine($"Service started streeming tweets for '{tracks}'. Press any key to stop.");
        Console.ReadKey();
    }
}