using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octokit;

// http://octokitnet.readthedocs.io
namespace OctokitTest
{
    class Program
    {
        private static GitHubClient _client;
        private static string _owner;
        private static string _repository;

        static void Main(string[] args)
        {
            _client = new GitHubClient(new ProductHeaderValue("LuxoftHackaton"));
            //var basicAuth = new Credentials("username", "password"); 
            //client.Credentials = basicAuth;

            _owner = "and85";
            _repository = "BloombergReaderWeb";

            GetReleases();
            GetIssues();
            GetUserInfo();
#if DEBUG
            Console.WriteLine("END");
            Console.ReadLine();
#endif
        }

        private static async void GetReleases()
        {
            var releases = await _client.Repository.Release.GetAll(_owner, _repository);
            Console.WriteLine("Get releases...");

            foreach (var release in releases)
            {
                Console.WriteLine(
                    "Release is tagged at {0} and is named {1} by {2}",
                    release.TagName,
                    release.Name,
                    release.Author.Login);
            }
        }

        private static async void GetIssues()
        {
            var issues = await _client.Issue.GetAllForRepository(_owner, _repository);
            Console.WriteLine("Get issues...");

            foreach (var issue in issues)
            {
                Console.WriteLine(
                    "issue.Id: {0}; issue.Assignee: {1}; issue.Number: {2}",
                    issue.Id,
                    issue.Assignee,
                    issue.Number);
            }
        }

        private static async void GetUserInfo()
        {
            var user = await _client.User.Get("and85");
            Console.WriteLine("Get user info...");

            Console.WriteLine(
                "User.Id: {0}; user.Name: {1}; user.Email: {2}; user.Location: {3}",
                user.Id,
                user.Name,
                user.Email,
                user.Location);
        }
    }
}
