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
using System;
using System.Linq;

namespace SanaraV2.Games
{
    public abstract class APreload
    {
        protected APreload(string[] names, int timer, Func<IGuild, string> gameSentence)
        {
            _names = names;
            _timer = timer;
            _gameSentence = gameSentence;
        }

        public abstract bool IsNsfw();
        public abstract bool DoesAllowFull(); // Allow 'full' attribute
        public abstract bool DoesAllowSendImage(); // Allow 'image' attribute
        public abstract bool DoesAllowCropped(); // Allow 'crop" attribute
        public abstract Shadow DoesAllowShadow(); // Allow 'shadow" attribute
        public abstract Multiplayer DoesAllowMultiplayer(); // Allow 'multi' attribute
        public abstract MultiplayerType GetMultiplayerType();
        /// <summary>
        /// If game allow multiplayer or not
        /// </summary>
        public enum Multiplayer
        {
            SoloOnly,
            MultiOnly,
            Both
        }
        /// <summary>
        /// Settings for shadow option: identify background
        /// </summary>
        public enum Shadow
        {
            Transparency, // Background is transparency
            White, // Background is white
            None // Shadow mode disabled
        }
        /// <summary>
        /// How multiplayer is handled
        /// </summary>
        public enum MultiplayerType
        {
            None, // No multiplayer available
            Elimination, // If player loose, he is eliminated
            BestOf // Player have X chances to find the solution, after Y question best score win
        }
        public abstract string GetRules(IGuild guildId, bool isMultiplayer);

        public bool ContainsName(string name)
            => _names.Contains(name);

        public int GetTimer()
            => _timer;

        public string GetGameName()
            => _names[0];

        public string GetGameSentence(IGuild guild)
            => _gameSentence(guild);

        private string[] _names;
        private int _timer;
        private Func<IGuild, string> _gameSentence;
    }
}
