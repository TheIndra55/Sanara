﻿/// This file is part of Sanara.
///
/// Sanara is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// Sanara is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with Sanara.  If not, see<http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SanaraV2.Features.Tools
{
    public static class Code
    {
        public static async Task<FeatureRequest<Response.Shell, Error.Shell>> Shell(string[] args)
        {
            if (args.Length == 0)
                return (new FeatureRequest<Response.Shell, Error.Shell>(null, Error.Shell.Help));
            string html;
            string url = "https://explainshell.com/explain?cmd=" + Uri.EscapeDataString(string.Join(" ", args));
            using (HttpClient hc = new HttpClient())
                html = await hc.GetStringAsync(url);
            if (html.Contains("No man page found for"))
                return (new FeatureRequest<Response.Shell, Error.Shell>(null, Error.Shell.NotFound));
            List<Tuple<string, string>> explanations = new List<Tuple<string, string>>();
            foreach (Match m in Regex.Matches(html, "<pre class=\"help-box\" id=\"help-[0-9]+\">"))
            {
                explanations.Add(new Tuple<string, string>("###",
                    ReduceSize(FormatShell(html.Split(new[] { m.Value }, StringSplitOptions.None)[1].Split(new[] { "</pre>" }, StringSplitOptions.None)[0]))));
            }
            return (new FeatureRequest<Response.Shell, Error.Shell>(new Response.Shell()
            {
                explanations = explanations,
                title = HttpUtility.HtmlDecode(Regex.Match(html, "<title>explainshell\\.com - ([^<]+)<\\/title>").Groups[1].Value),
                url = "https://explainshell.com/explain?cmd=" + Uri.EscapeDataString(string.Join(" ", args))
            }, Error.Shell.None));
        }

        /// Clean input to put bold and stuffs depending HTML
        private static string FormatShell(string input)
            => HttpUtility.HtmlDecode(
                Regex.Replace(
                    Regex.Replace(
                        input,
                        "<b>([^<]+)<\\/b>", "**$1**"),
                    "<u>([^<]+)<\\/u>", "__$1__"))
                .Replace("\n\n", "%5Cn").Replace("\n", " ").Replace("%5Cn", "\n\n");

        /// We make sure that the input is less than 1024 characters, if it's bigger we split by .
        private static string ReduceSize(string input)
        {
            while (input.Length > 1024)
            {
                string[] tmp = input.Split('.');
                input = string.Join(".", tmp.Take(tmp.Length - 1));
            }
            return input;
        }
    }
}