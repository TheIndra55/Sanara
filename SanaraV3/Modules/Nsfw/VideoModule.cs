using Discord;
using Discord.Commands;
using SanaraV3.Exceptions;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SanaraV3.Modules.Nsfw
{
    public sealed class VideoModule : ModuleBase, IModule
    {
        public string ModuleName { get { return "Nsfw"; } }

        [Command("AdultVideo", RunMode = RunMode.Async), RequireNsfw, Alias("AV")]
        public async Task AdultVideoAsync(string tag = "")
        {
            tag = tag.ToLowerInvariant();
            if (tag == "")
                tag = "all";
            else if (!StaticObjects.JavmostCategories.Contains(tag))
                throw new CommandFailed("This tag doesn't exist");

            string url = "https://www5.javmost.com/category/" + tag;
            var request = new HttpRequestMessage(new HttpMethod("GET"), url);
            request.Headers.Add("Host", "javmost.com");
            string html = await (await StaticObjects.HttpClient.SendAsync(request)).Content.ReadAsStringAsync();
            int perPage = Regex.Matches(html, "<!-- begin card -->").Count; // Number of result per page
            System.Console.WriteLine(html);
            System.Console.WriteLine(url);
            int total = int.Parse(Regex.Match(html, "<input type=\"hidden\" id=\"page_total\" value=\"([0-9]+)\" \\/>").Groups[1].Value); // Total number of video
            int page = StaticObjects.Random.Next(total / perPage);
            if (page > 0) // If it's the first page, we already got the HTML
            {
                request = new HttpRequestMessage(new HttpMethod("GET"), url + "/page/" + (page + 1));
                request.Headers.Add("Host", "javmost.com");
                html = await (await StaticObjects.HttpClient.SendAsync(request)).Content.ReadAsStringAsync();
            }
            var arr = html.Split(new[] { "<!-- begin card -->" }, StringSplitOptions.None).Skip(1).ToList(); // We remove things life header and stuff
            Match videoMatch = null;
            string[] videoTags = null;
            string previewUrl = "";
            while (arr.Count > 0) // That shouldn't fail
            {
                string currHtml = arr[StaticObjects.Random.Next(arr.Count)];
                videoMatch = Regex.Match(currHtml, "<a href=\"(https:\\/\\/www5\\.javmost\\.com\\/([^\\/]+)\\/)\"");
                if (!videoMatch.Success)
                    continue;
                videoMatch = Regex.Match(currHtml, "<a href=\"(https:\\/\\/www5\\.javmost\\.com\\/([^\\/]+)\\/)\"");
                previewUrl = Regex.Match(currHtml, "data-src=\"([^\"]+)\"").Groups[1].Value;
                if (previewUrl.StartsWith("//"))
                    previewUrl = "https:" + previewUrl;
                videoTags = Regex.Matches(currHtml, "<a href=\"https:\\/\\/www5\\.javmost\\.com\\/category\\/([^\\/]+)\\/\"").Cast<Match>().Select(x => x.Groups[1].Value).ToArray();
                break;
            }
            await ReplyAsync(embed: new EmbedBuilder()
            {
                Color = new Color(255, 20, 147),
                Description = string.Join(", ", videoTags),
                Title = videoMatch.Groups[2].Value,
                Url = videoMatch.Groups[1].Value,
                ImageUrl = previewUrl
            }.Build());
        }
    }
}
