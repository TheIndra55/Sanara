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

using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SanaraV2.Games.Impl
{
    public class KanCollePreload : APreload
    {
        public KanCollePreload() : base(new[] { "kancolle" }, 15, Sentences.KancolleGame)
        { }

        public override bool IsNsfw()
            => false;

        public override bool DoesAllowFull()
            => false;

        public override string GetRules(ulong guildId)
            => Sentences.RulesKancolle(guildId);
    }

    public class KanColle : AQuizz
    {
        public KanColle(ITextChannel chan, List<string> dictionnary, Config config) : base(chan, dictionnary, config)
        { }

        protected override async Task<string[]> GetPostInternalAsync()
        {
            using (HttpClient hc = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                dynamic json = JsonConvert.DeserializeObject(await hc.GetStringAsync("https://kancolle.fandom.com/api/v1/Search/List?query=" + Uri.EscapeDataString(_toGuess) + "&limit=1"));
                string shipUrl = json.items[0].url + "/Gallery";
                string html = await hc.GetStringAsync(shipUrl);
                return (new string[] { html.Split(new string[] { "img src=\"" }, StringSplitOptions.None)[2].Split('"')[0].Split(new string[] { "/revision" }, StringSplitOptions.None)[0] });
            }
        }

        public List<string> LoadDictionnary()
        {
            List<string> ships = new List<string>();
            using (HttpClient hc = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string json = hc.GetStringAsync("https://kancolle.fandom.com/wiki/Ship").GetAwaiter().GetResult();
                json = json.Split(new string[] { "List_of_coastal_defense_ships_by_upgraded_maximum_stats" }, StringSplitOptions.None)[1].Split(new string[] { "Fleet_of_Fog" }, StringSplitOptions.None)[0];
                MatchCollection matches = Regex.Matches(json, "<a href=\"\\/wiki\\/([^\"]+)\" title=\"[^\"]+\">[^<]+<\\/a>");
                foreach (Match match in matches)
                {
                    string str = match.Groups[1].Value.Replace("'", "").Split('|')[0];
                    if (!str.StartsWith("List_of") && !str.StartsWith("Category:") && !ships.Contains(str))
                        ships.Add(str);
                }
            }
            return (ships);
        }
    }
}
