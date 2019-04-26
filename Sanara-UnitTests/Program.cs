﻿using Xunit;
using System;
using SanaraV2.Features.NSFW;
using SanaraV2.Features.Entertainment;
using System.Net;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.IO;

namespace Sanara_UnitTests
{
    public class Program
    {
        private static bool IsLinkValid(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "HEAD";
                    request.GetResponse();
                    return (true);
                }
                catch (WebException)
                { }
            }
            return (false);
        }

        // ANIME/MANGA MODULE
        [Fact]
        public async Task TestAnime()
        {
            var result = await AnimeManga.SearchAnime(true, ("Gochuumon wa Usagi desu ka?").Split(' '));
            Assert.Equal(SanaraV2.Features.Entertainment.Error.AnimeManga.None, result.error);
            Assert.NotNull(result.answer);
            Assert.Equal("Gochuumon wa Usagi desu ka?", result.answer.name);
            Assert.Equal("https://media.kitsu.io/anime/poster_images/8095/original.jpg?1408463456", result.answer.imageUrl);
            Assert.Equal("GochiUsa", string.Join("", result.answer.alternativeTitles));
            Assert.Equal(12, result.answer.episodeCount);
            Assert.Equal(23, result.answer.episodeLength);
            Assert.InRange(result.answer.rating.Value, 60, 90);
            Assert.Equal(new DateTime(2014, 4, 10), result.answer.startDate);
            Assert.Equal(new DateTime(2014, 6, 26), result.answer.endDate);
            Assert.Equal("Teens 13 or older", result.answer.ageRating);
            Assert.InRange(result.answer.synopsis.Length, 800, 1200);
        }

        // DOUJINSHI MODULE
        [Fact]
        public async Task TestDoujinshi()
        {
           var result = await Doujinshi.SearchDoujinshi(false, new string[] { "color", "english" }, new Random());
            Assert.Equal(SanaraV2.Features.NSFW.Error.Doujinshi.None, result.error);
            Assert.True(IsLinkValid(result.answer.url));
        }

        // GAME MODULE
        [Fact]
        public async Task TestBooruGame()
        {
            var dict =  Game.LoadBooru();
            Assert.NotNull(dict);
        }

        [Fact]
        public async Task TestShiritoriGame()
        {
            var dict = Game.LoadShiritori();
            Assert.NotNull(dict);
        }

        [Fact]
        public async Task TestAnimeGame()
        {
            var dict = Game.LoadAnime();
            Assert.NotNull(dict);
        }

        [Fact]
        public async Task TestKancolleGame()
        {
            var dict = await Game.LoadKancolle();
            Assert.NotNull(dict);
            Assert.True(dict.Count > 220);
        }

        [Fact]
        public async Task TestAzurLaneGame()
        {
            var dict = await Game.LoadAzurLane();
            Assert.NotNull(dict);
            Assert.True(dict.Count > 300);
        }

        // BOORU MODULE
        [Fact]
        public async Task TestBooruSafe()
        {
            var result = await Booru.SearchBooru(false, null, new BooruSharp.Booru.Safebooru(), new Random());
            Assert.Equal(SanaraV2.Features.NSFW.Error.Booru.None, result.error);
            Assert.Equal(Discord.Color.Green, result.answer.colorRating);
            Assert.True(IsLinkValid(result.answer.url));
        }

        [Fact]
        public async Task TestBooruNotSafe()
        {
            var result = await Booru.SearchBooru(false, new string[] { "cum_in_pussy" }, new BooruSharp.Booru.Gelbooru(), new Random());
            Assert.Equal(SanaraV2.Features.NSFW.Error.Booru.None, result.error);
            Assert.Equal(Discord.Color.Red, result.answer.colorRating);
            Assert.True(IsLinkValid(result.answer.url));
        }

        [Fact]
        public async Task TestBooruTag()
        {
            BooruSharp.Booru.Gelbooru booru = new BooruSharp.Booru.Gelbooru();
            Random rand = new Random();
            var resultSearch = await Booru.SearchBooru(false, new string[] { "hibiki_(kantai_collection)" }, booru, rand);
            var resultTags = await Booru.SearchTags(new string[] { resultSearch.answer.saveId.ToString() });
            Assert.Equal(SanaraV2.Features.NSFW.Error.BooruTags.None, resultTags.error);
            Assert.Contains("hibiki_(kantai_collection)", resultTags.answer.characTags);
            Assert.Contains("kantai_collection", resultTags.answer.sourceTags);
            Assert.Equal("Gelbooru", resultTags.answer.booruName);
        }

        [SkipIfNoToken]
        public async Task IntegrationTest()
        {
            string ayamiToken, inamiToken;
            if (File.Exists("Keys/ayamiToken.txt"))
                ayamiToken = File.ReadAllText("Keys/ayamiToken.txt");
            else
                ayamiToken = Environment.GetEnvironmentVariable("AYAMI_TOKEN");
            if (File.Exists("Keys/inamiToken.txt"))
                inamiToken = File.ReadAllText("Keys/inamiToken.txt");
            else
                inamiToken = Environment.GetEnvironmentVariable("INAMI_TOKEN");
            DiscordSocketClient ayamiClient = new DiscordSocketClient();
            DiscordSocketClient inamiClient = new DiscordSocketClient();
            await ayamiClient.LoginAsync(Discord.TokenType.Bot, ayamiToken);
            await inamiClient.LoginAsync(Discord.TokenType.Bot, inamiToken);
            await ayamiClient.StartAsync();
            await inamiClient.StartAsync();
        }
    }

    public sealed class SkipIfNoToken : FactAttribute
    {
        public SkipIfNoToken()
        {
            if (!File.Exists("Keys/ayamiToken.txt") && Environment.GetEnvironmentVariable("AYAMI_TOKEN") == null
                && !File.Exists("Keys/inamiToken.txt") && Environment.GetEnvironmentVariable("INAMI_TOKEN") == null)
                Skip = "No token found in files or environment variables";
        }
    }
}
