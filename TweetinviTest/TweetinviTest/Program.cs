using System;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Events;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

// https://apps.twitter.com/app/13131014
// https://github.com/linvi/tweetinvi/wiki
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
        Auth.SetCredentials(credentials);

        //SubscribeToTweets(credentials);
        //GetTimeline();
        //SimpleSearch();
        //AdvancedSearch();

        SearchUser();

        Console.ReadKey();
    }

    private static void SearchUser()
    {
        var users = Search.SearchUsers("tkachandriiol");
        foreach (var user in users)
        {
            Console.WriteLine(user.Name);
            Console.WriteLine(user.ProfileImageUrl);
        }
    }

    private static void SimpleSearch()
    {

        var matchingTweets = Search.SearchTweets("tkachandrii");
        PrintTweets(matchingTweets);
    }

    // I think the best option will be to search tweets by id
    private static void AdvancedSearch()
    {
        var searchParameter = new SearchTweetsParameters("tkachandrii")
        {
            //Lang = LanguageFilter.English,
            //SearchType = SearchResultType.Recent,
            //MaximumNumberOfResults = 100,
            //Until = new DateTime(2016, 11, 24)
            Since = new DateTime(2016, 11, 24)
            //SinceId = 399616835892781056,
            //MaxId = 405001488843284480,
            //Filters = TweetSearchFilters.Images
        };

        var tweets = Search.SearchTweets(searchParameter);
        PrintTweets(tweets);
    }

    private static void GetTimeline()
    {
        var tweets = Timeline.GetHomeTimeline();
        PrintTweets(tweets);
    }

    private static void PrintTweets(System.Collections.Generic.IEnumerable<ITweet> tweets)
    {
        foreach (var tweet in tweets)
        {
            try
            {
                Console.WriteLine(tweet.Text);
                Console.WriteLine(tweet.CreatedBy);
                Console.WriteLine(tweet.Urls[0].ExpandedURL);
            }
            catch
            {

            }
        }
    }

    private static void SubscribeToTweets(TwitterCredentials credentials)
    {
        var tracks = "#tkachandrii".Split(',').ToArray();

        var stream = Stream.CreateFilteredStream();
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
    }
}