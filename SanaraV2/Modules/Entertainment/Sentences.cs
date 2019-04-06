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
using SanaraV2.Modules.Base;

namespace SanaraV2.Modules.Entertainment
{
    public static class Sentences
    {
        /// --------------------------- Games ---------------------------
        public static string RulesShiritori(ulong guildId) { return (Translation.GetTranslation(guildId, "rulesShiritori")); }
        public static string RulesKancolle(ulong guildId) { return (Translation.GetTranslation(guildId, "rulesKancolle")); }
        public static string RulesBooru(ulong guildId) { return (Translation.GetTranslation(guildId, "rulesBooru")); }
        public static string RulesAnime(ulong guildId) { return (Translation.GetTranslation(guildId, "rulesAnime")); }
        public static string RulesFireEmblem(ulong guildId) { return (Translation.GetTranslation(guildId, "rulesFireEmblem")); }
        public static string RulesTimer(ulong guildId, int timerRef) { return (Translation.GetTranslation(guildId, "rulesTimer", timerRef.ToString())); }
        public static string RulesReset(ulong guildId) { return (Translation.GetTranslation(guildId, "rulesReset")); }
        public static string ResetNone(ulong guildId) { return (Translation.GetTranslation(guildId, "resetNone")); }
        public static string ResetDone(ulong guildId) { return (Translation.GetTranslation(guildId, "resetDone")); }
        public static string InvalidGameName(ulong guildId) { return (Translation.GetTranslation(guildId, "invalidGameName")); }
        public static string GameAlreadyRunning(ulong guildId) { return (Translation.GetTranslation(guildId, "gameAlreadyRunning")); }
        public static string TimeoutGame(ulong guildId, string answer) { return (Translation.GetTranslation(guildId, "timeoutGame", answer)); }
        public static string NewBestScore(ulong guildId, string lastScore, string newScore) { return (Translation.GetTranslation(guildId, "newBestScore", lastScore, newScore)); }
        public static string EqualizedScore(ulong guildId, string score) { return (Translation.GetTranslation(guildId, "equalizedScore", score)); }
        public static string DidntBeatScore(ulong guildId, string lastScore, string newScore) { return (Translation.GetTranslation(guildId, "didntBeatScore", lastScore, newScore)); }
        public static string ShiritoriNoWord(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriNoWord")); }
        public static string OnlyHiraganaKatakanaRomaji(ulong guildId) { return (Translation.GetTranslation(guildId, "onlyHiraganaKatakanaRomaji")); }
        public static string ShiritoriNotNoun(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriNotNoun")); }
        public static string ShiritoriDoesntExist(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriDoesntExist")); }
        public static string ShiritoriMustBegin(ulong guildId, string beginHiragana, string beginRomaji) { return (Translation.GetTranslation(guildId, "shiritoriMustBegin", beginHiragana, beginRomaji)); }
        public static string ShiritoriAlreadySaid(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriAlreadySaid")); }
        public static string ShiritoriEndWithN(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriEndWithN")); }
        public static string ShiritoriNoMoreWord(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriNoMoreWord")); }
        public static string ShiritoriSuggestion(ulong guildId, string suggestionHiragana, string suggestionRomaji, string suggestionTranslation) { return (Translation.GetTranslation(guildId, "shiritoriSuggestion", suggestionHiragana, suggestionRomaji, suggestionTranslation)); }
        public static string WaitImage(ulong guildId) { return (Translation.GetTranslation(guildId, "waitImage")); }
        public static string KancolleGuessDontExist(ulong guildId) { return (Translation.GetTranslation(guildId, "kancolleGuessDontExist")); }
        public static string FireEmblemGuessDontExist(ulong guildId) { return (Translation.GetTranslation(guildId, "fireEmblemGuessDontExist")); }
        public static string GuessGood(ulong guildId) { return (Translation.GetTranslation(guildId, "guessGood")); }
        public static string GuessBad(ulong guildId, string attempt) { return (Translation.GetTranslation(guildId, "guessBad", attempt)); }
        public static string BooruGuessClose(ulong guildId, string attempt) { return (Translation.GetTranslation(guildId, "booruGuessClose", attempt)); }
        public static string LostStr(ulong guildId) { return (Translation.GetTranslation(guildId, "lost")); }
        public static string InvalidDifficulty(ulong guildId) { return (Translation.GetTranslation(guildId, "invalidDifficulty")); }
        public static string NoDictionnary(ulong guildId) { return (Translation.GetTranslation(guildId, "noDictionnary")); }
        public static string ExceptionGame(ulong guildId, string url) { return (Translation.GetTranslation(guildId, "exceptionGame", url)); }
        public static string ExceptionGameStop(ulong guildId) { return (Translation.GetTranslation(guildId, "exceptionGameStop")); }
        public static string AnimeGame(ulong guildId) { return (Translation.GetTranslation(guildId, "animeGame")); }
        public static string BooruGame(ulong guildId) { return (Translation.GetTranslation(guildId, "animeGame")); }
        public static string KancolleGame(ulong guildId) { return (Translation.GetTranslation(guildId, "kancolleGame")); }
        public static string ShiritoriGame(ulong guildId) { return (Translation.GetTranslation(guildId, "shiritoriGame")); }
        public static string ScoreText(ulong guildId, int rank, int total, int score) { return (Translation.GetTranslation(guildId, "scoreText", rank.ToString(), total.ToString(), score.ToString())); }
        public static string ScoreContributors(ulong guildId) { return (Translation.GetTranslation(guildId, "scoreContributors")); }

        /// --------------------------- AnimeManga ---------------------------
        public static string MangaHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "mangaHelp")); }
        public static string AnimeHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "animeHelp")); }
        public static string MangaNotFound(ulong guildId) { return (Translation.GetTranslation(guildId, "mangaNotFound")); }
        public static string AnimeNotFound(ulong guildId) { return (Translation.GetTranslation(guildId, "animeNotFound")); }
        public static string AnimeEpisodes(ulong guildId) { return (Translation.GetTranslation(guildId, "animeEpisodes")); }
        public static string AnimeLength(ulong guildId, int length) { return (Translation.GetTranslation(guildId, "animeLength", length.ToString())); }
        public static string AnimeRating(ulong guildId) { return (Translation.GetTranslation(guildId, "animeRating")); }
        public static string AnimeAudiance(ulong guildId) { return (Translation.GetTranslation(guildId, "animeAudiance")); }
        public static string ToBeAnnounced(ulong guildId) { return (Translation.GetTranslation(guildId, "toBeAnnounced")); }
        public static string Unknown(ulong guildId) { return (Translation.GetTranslation(guildId, "unknown")); }

        /// --------------------------- Radio ---------------------------
        public static string RadioAlreadyStarted(ulong guildId) { return (Translation.GetTranslation(guildId, "radioAlreadyStarted")); }
        public static string RadioNeedChannel(ulong guildId) { return (Translation.GetTranslation(guildId, "radioNeedChannel")); }
        public static string RadioNeedArg(ulong guildId) { return (Translation.GetTranslation(guildId, "radioNeedArg")); }
        public static string RadioNotStarted(ulong guildId) { return (Translation.GetTranslation(guildId, "radioNotStarted")); }
        public static string RadioAlreadyInList(ulong guildId) { return (Translation.GetTranslation(guildId, "radioAlreadyInList")); }
        public static string RadioTooMany(ulong guildId) { return (Translation.GetTranslation(guildId, "radioTooMany")); }
        public static string RadioNoSong(ulong guildId) { return (Translation.GetTranslation(guildId, "radioNoSong")); }
        public static string SongSkipped(ulong guildId, string songName) { return (Translation.GetTranslation(guildId, "songSkipped", songName)); }
        public static string Current(ulong guildId) { return (Translation.GetTranslation(guildId, "current")); }
        public static string Downloading(ulong guildId) { return (Translation.GetTranslation(guildId, "downloading")); }
        public static string SongAdded(ulong guildId, string songName) { return (Translation.GetTranslation(guildId, "songAdded", songName)); }

        /// --------------------------- VN ---------------------------
        public static string VndbHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "vndbHelp")); }
        public static string VndbNotFound(ulong guildId) { return (Translation.GetTranslation(guildId, "vndbNotFound")); }
        public static string AvailableEnglish(ulong guildId) { return (Translation.GetTranslation(guildId, "availableEnglish")); }
        public static string AvailableWindows(ulong guildId) { return (Translation.GetTranslation(guildId, "availableWindows")); }
        public static string VndbRating(ulong guildId) { return (Translation.GetTranslation(guildId, "vndbRating")); }
        public static string Hours(ulong guildId, string length) { return (Translation.GetTranslation(guildId, "hours", length)); }
        public static string Length(ulong guildId) { return (Translation.GetTranslation(guildId, "length")); }
        public static string Tba(ulong guildId) { return (Translation.GetTranslation(guildId, "tba")); }
        public static string ReleaseDate(ulong guildId) { return (Translation.GetTranslation(guildId, "releaseDate")); }

        /// --------------------------- XKCD ---------------------------
        public static string XkcdWrongArg(ulong guildId) { return (Translation.GetTranslation(guildId, "xkcdWrongArg")); }
        public static string XkcdWrongId(ulong guildId, int max) { return (Translation.GetTranslation(guildId, "xkcdWrongId", max.ToString())); }

        /// --------------------------- Youtube ---------------------------
        public static string YoutubeHelp(ulong guildId) { return (Translation.GetTranslation(guildId, "youtubeHelp")); }
        public static string YoutubeNotFound(ulong guildId) { return (Translation.GetTranslation(guildId, "youtubeNotFound")); }
    }
}