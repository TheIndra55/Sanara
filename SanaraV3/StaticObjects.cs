using BooruSharp.Booru;
using Discord;
using DiscordUtils;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using SanaraV3.Modules.Game;
using SanaraV3.Modules.Game.PostMode;
using SanaraV3.Modules.Game.Preload;
using SanaraV3.Modules.Game.Preload.Shiritori;
using SanaraV3.Modules.Nsfw;
using SanaraV3.Modules.Radio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace SanaraV3
{
    /// <summary>
    /// Keep track of all the objects that must be keepen alive
    /// </summary>
    public static class StaticObjects
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        public static readonly Random Random = new Random();

        // NSFW MODULE
        public static readonly Safebooru Safebooru = new Safebooru();
        public static readonly Gelbooru Gelbooru = new Gelbooru();
        public static readonly E621 E621 = new E621();
        public static readonly E926 E926 = new E926();
        public static readonly Rule34 Rule34 = new Rule34();
        public static readonly Konachan Konachan = new Konachan();
        public static readonly TagsManager Tags = new TagsManager();
        public static readonly List<string> JavmostCategories = new List<string>();

        // RADIO MODULE
        public static readonly Dictionary<ulong, RadioChannel> Radios = new Dictionary<ulong, RadioChannel>();

        // ENTERTAINMENT MODULE
        public static YouTubeService YouTube = null;

        // GAME MODULE
        public static readonly List<AGame> Games = new List<AGame>();
        public static readonly TextMode ModeText = new TextMode();
        public static readonly UrlMode ModeUrl = new UrlMode();
        public static readonly IPreload[] Preloads;
        private static readonly GameManager _gm;

        // LANGUAGE MODULE
        public static readonly Dictionary<string, string> RomajiToHiragana = new Dictionary<string, string>();
        public static readonly Dictionary<string, string> HiraganaToRomaji = new Dictionary<string, string>();
        public static readonly Dictionary<string, string> RomajiToKatakana = new Dictionary<string, string>();
        public static readonly Dictionary<string, string> KatakanaToRomaji = new Dictionary<string, string>();

        static StaticObjects()
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 Sanara");

                Safebooru.HttpClient = HttpClient;
                Gelbooru.HttpClient = HttpClient;
                E621.HttpClient = HttpClient;
                E926.HttpClient = HttpClient;
                Rule34.HttpClient = HttpClient;
                Konachan.HttpClient = HttpClient;

                RomajiToHiragana = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("LanguageResources/Hiragana.json"));
                foreach (var elem in RomajiToHiragana)
                {
                    HiraganaToRomaji.Add(elem.Value, elem.Key);
                }
                RomajiToKatakana = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("LanguageResources/Katakana.json"));
                foreach (var elem in RomajiToKatakana)
                {
                    KatakanaToRomaji.Add(elem.Value, elem.Key);
                }

                Preloads = new[]
                {
                    new ShiritoriPreload()
                };
                _gm = new GameManager();

                // There 2 tags aren't in the tag list so we add them manually
                JavmostCategories.Add("censor");
                JavmostCategories.Add("uncensor");
                List<string> newTags;
                int page = 1;
                do
                {
                    newTags = new List<string>(); // We keep track of how many tags we found in this page
                    var request = new HttpRequestMessage(new HttpMethod("GET"), "https://www5.javmost.com/allcategory/" + page);
                    request.Headers.Add("Host", "javmost.com"); // Javmost keep redirecting me if I don't send this
                    string html = HttpClient.SendAsync(request).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    foreach (Match m in Regex.Matches(html, "<a href=\"https:\\/\\/www5\\.javmost\\.com\\/category\\/([^\\/]+)\\/\">").Cast<Match>())
                    {
                        string content = m.Groups[1].Value.Trim().ToLower();
                        if (!JavmostCategories.Contains(content)) // Make sure to not add things twice
                            newTags.Add(content);
                    }
                    JavmostCategories.AddRange(newTags);
                    page++;
                } while (newTags.Count > 0);
            }
            catch (Exception e) // We can't really allow this to throw an exception since it would prevent the bot to start
            {
                Utils.LogError(new LogMessage(LogSeverity.Critical, e.Source, e.Message, e));
            }
        }

        public static void Initialize(Credentials credentials)
        {
            if (credentials.YouTubeKey != null)
            {
                YouTube = new YouTubeService(new BaseClientService.Initializer
                {
                    ApiKey = credentials.YouTubeKey
                });
            }
        }
    }
}
