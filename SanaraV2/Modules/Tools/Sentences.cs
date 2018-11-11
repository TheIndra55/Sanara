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
using SanaraV2.Modules.Base;
using System;

namespace SanaraV2.Modules.Tools
{
    public static class Sentences
    {
        /// --------------------------- Code ---------------------------
        public static string IndenteHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "indenteHelp")); }

        /// --------------------------- Communication ---------------------------
        public static string IntroductionMsg(ulong guildId) { return (Translation.GetTranslation(guildId, "introductionMsg")); }
        public static string HiStr(ulong guildId) { return (Translation.GetTranslation(guildId, "hi")); }
        public static string WhoIAmStr(ulong guildId) { return (Translation.GetTranslation(guildId, "whoIAm")); }
        public static string UserNotExist(ulong guildId) { return (Translation.GetTranslation(guildId, "userNotExist")); }
        public static string Username(ulong guildId) { return (Translation.GetTranslation(guildId, "username")); }
        public static string Nickname(ulong guildId) { return (Translation.GetTranslation(guildId, "nickname")); }
        public static string AccountCreation(ulong guildId) { return (Translation.GetTranslation(guildId, "accountCreation")); }
        public static string GuildJoined(ulong guildId) { return (Translation.GetTranslation(guildId, "guildJoined")); }
        public static string Creator(ulong guildId) { return (Translation.GetTranslation(guildId, "creator")); }
        public static string Uptime(ulong guildId) { return (Translation.GetTranslation(guildId, "uptime")); }
        public static string Website(ulong guildId) { return (Translation.GetTranslation(guildId, "website")); }
        public static string OfficialGuild(ulong guildId) { return (Translation.GetTranslation(guildId, "officialGuild")); }
        public static string Roles(ulong guildId) { return (Translation.GetTranslation(guildId, "roles")); }
        public static string NoRole(ulong guildId) { return (Translation.GetTranslation(guildId, "noRole")); }
        public static string LatestVersion(ulong guildId) { return (Translation.GetTranslation(guildId, "latestVersion")); }
        public static string NumberGuilds(ulong guildId) { return (Translation.GetTranslation(guildId, "numberGuilds")); }
        public static string QuoteInvalidId(ulong guildId) { return (Translation.GetTranslation(guildId, "quoteInvalidId")); }
        public static string QuoteNoMessage(ulong guildId) { return (Translation.GetTranslation(guildId, "quoteNoMessage")); }

        /// --------------------------- Image ---------------------------
        public static string InvalidColor(ulong guildId) { return (Translation.GetTranslation(guildId, "invalidColor")); }
        public static string HelpColor(ulong guildId) { return (Translation.GetTranslation(guildId, "helpColor")); }

        /// --------------------------- Linguist ---------------------------
        public static string ToHiraganaHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "toHiraganaHelp")); }
        public static string ToRomajiHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "toRomajiHelp")); }
        public static string ToKatakanaHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "toKatakanaHelp")); }
        public static string JapaneseHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "japaneseHelp")); }
        public static string TranslateHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "translateHelp")); }
        public static string InvalidLanguage(ulong guildId) { return (Translation.GetTranslation(guildId, "invalidLanguage")); }
        public static string GiveJapaneseTranslations(ulong guildId, string word) { return (Translation.GetTranslation(guildId, "giveJapaneseTranslations", word)); }
        public static string NoJapaneseTranslation(ulong guildId) { return (Translation.GetTranslation(guildId, "noJapaneseTranslation")); }
        public static string Meaning(ulong guildId) { return (Translation.GetTranslation(guildId, "meaning")); }
        public static string NoTextOnImage(ulong guildId) { return (Translation.GetTranslation(guildId, "noTextOnImage")); }
        public static string NotAnImage(ulong guildId) { return (Translation.GetTranslation(guildId, "notAnImage")); }

        /// --------------------------- Settings ---------------------------
        public static string CreateArchiveStr(ulong guildId, string currTime)
        { return (Translation.GetTranslation(guildId, "createArchive", currTime)); }
        public static string CopyingFiles(ulong guildId) { return (Translation.GetTranslation(guildId, "copyingFiles")); }
        public static string NeedLanguage(ulong guildId) { return (Translation.GetTranslation(guildId, "needLanguage")); }
        public static string PrefixRemoved(ulong guildId) { return (Translation.GetTranslation(guildId, "prefixRemoved")); }
        
        /// --------------------------- Help ---------------------------
        private static string NoCommandAvailable(ulong guildId) { return (Translation.GetTranslation(guildId, "noCommandAvailable")); }
        public static Embed Help(ulong guildId, bool isChanNsfw)
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = Translation.GetTranslation(guildId, "help"),
                Color = Color.Purple
            };
            embed.AddField(Translation.GetTranslation(guildId, "animeMangaModuleName"), Translation.GetTranslation(guildId, "animeMangaModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "animeMangaModuleDescription2"));
            embed.AddField(Translation.GetTranslation(guildId, "booruModuleName"), Translation.GetTranslation(guildId, "booruModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "booruModuleDescription5")
                + ((isChanNsfw) ? (Environment.NewLine + Translation.GetTranslation(guildId, "booruModuleDescription2") + Environment.NewLine + Translation.GetTranslation(guildId, "booruModuleDescription3") + Environment.NewLine + Translation.GetTranslation(guildId, "booruModuleDescription4") + Environment.NewLine + Translation.GetTranslation(guildId, "booruModuleDescription6")) : ("")));
            embed.AddField(Translation.GetTranslation(guildId, "communicationModuleName"), Translation.GetTranslation(guildId, "communicationModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "communicationModuleDescription2") + Environment.NewLine + Translation.GetTranslation(guildId, "communicationModuleDescription3") + Environment.NewLine + Translation.GetTranslation(guildId, "communicationModuleDescription4") + Environment.NewLine + Translation.GetTranslation(guildId, "communicationModuleDescription5"));
            embed.AddField(Translation.GetTranslation(guildId, "doujinshiModuleName"),
                ((isChanNsfw) ? (Translation.GetTranslation(guildId, "doujinshiModuleDescription"))
                              : (NoCommandAvailable(guildId))));
            embed.AddField(Translation.GetTranslation(guildId, "gameModuleName"), Translation.GetTranslation(guildId, "gameModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "gameModuleDescription2") + Environment.NewLine + Translation.GetTranslation(guildId, "gameModuleDescription3")
                + ((isChanNsfw) ? (Environment.NewLine + Translation.GetTranslation(guildId, "gameModuleDescription4")) : (""))
                + Environment.NewLine + Translation.GetTranslation(guildId, "gameModuleDescription5"));
            embed.AddField(Translation.GetTranslation(guildId, "imageModuleName"), Translation.GetTranslation(guildId, "imageModuleDescription1"));
            embed.AddField(Translation.GetTranslation(guildId, "kantaiCollectionModuleName"), Translation.GetTranslation(guildId, "kantaiCollectionModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "kantaiCollectionModuleDescription2") + Environment.NewLine + Translation.GetTranslation(guildId, "kantaiCollectionModuleDescription3"));
            embed.AddField(Translation.GetTranslation(guildId, "linguisticModuleName"),Translation.GetTranslation(guildId, "linguisticModuleDescription4") + Environment.NewLine + Translation.GetTranslation(guildId, "linguisticModuleDescription5"));
            embed.AddField(Translation.GetTranslation(guildId, "radioModuleName"), Translation.GetTranslation(guildId, "radioModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "radioModuleDescription2") + Environment.NewLine + Translation.GetTranslation(guildId, "radioModuleDescription3") + Environment.NewLine + Translation.GetTranslation(guildId, "radioModuleDescription4") + Environment.NewLine + Translation.GetTranslation(guildId, "radioModuleDescription5"));
            embed.AddField(Translation.GetTranslation(guildId, "settingsModuleName"), Translation.GetTranslation(guildId, "settingsModuleDescription1") + Environment.NewLine + Translation.GetTranslation(guildId, "settingsModuleDescription2"));
            embed.AddField(Translation.GetTranslation(guildId, "visualNovelModuleName"), Translation.GetTranslation(guildId, "visualNovelModuleDescription"));
            embed.AddField(Translation.GetTranslation(guildId, "xkcdModuleName"), Translation.GetTranslation(guildId, "xkcdModuleDescription"));
            embed.AddField(Translation.GetTranslation(guildId, "youtubeModuleName"), Translation.GetTranslation(guildId, "youtubeModuleDescription") + Environment.NewLine + Environment.NewLine
                + ((isChanNsfw) ? ("") : (Translation.GetTranslation(guildId, "nsfwForFull"))));
            return (embed.Build());
        }
    }
}