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
            //_repository = "HackerRank";

            //GetReleases();
            //GetIssues();
            //GetUserInfo();
            //GetCommits();
            GetRating();
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

        private static async void GetRating()
        {
            var repository = await _client.Repository.Get(_owner, _repository);
            
            Console.WriteLine("Get rating...");
            Console.WriteLine(repository.StargazersCount);
        }

        private static async void GetCommits()
        {
            var commits = await _client.Repository.Commit.GetAll(_owner, _repository);
            Console.WriteLine("Get commits...");

            foreach (var commit in commits)
            {
                Console.WriteLine(
                    "commit.Commit.Message,: {0}; commit.Commit.Author: {1}",
                    commit.Commit.Message,
                    commit.Commit.Author.Name);
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
                "User.Id: {0}; user.Name: {1}; user.Email: {2}; user.Location: {3}, user.AvatarUrl: {4}",
                user.Id,
                user.Name,
                user.Email,
                user.Location,
                user.AvatarUrl);
        }
    }
}
