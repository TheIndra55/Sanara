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
namespace SanaraV2.Games
{
    public struct Config
    {
        public Config(int refTime, Difficulty difficulty, string gameName, bool isFull, bool sendImage, bool isCropped,
            APreload.Shadow isShaded, APreload.Multiplayer isMultiplayer, APreload.MultiplayerType multiplayerType)
        {
            this.refTime = refTime;
            this.difficulty = difficulty;
            this.gameName = gameName;
            this.isFull = isFull;
            this.sendImage = sendImage;
            this.isCropped = isCropped;
            this.isShaded = isShaded;
            this.isMultiplayer = isMultiplayer;
            this.multiplayerType = multiplayerType;
        }

        public int refTime; // Time before the counter end and the player loose
        public Difficulty difficulty;
        public string gameName; // Used to store the score in the db
        public bool isFull; // Some game have a full mode containing a bigger dictionnary (not filtered)
        public bool sendImage; // For anime game, send images instead of video
        public bool isCropped; // Difficulty level cutting images in half
        public APreload.Shadow isShaded; // Difficulty level only displaying shadow
        public APreload.Multiplayer isMultiplayer; // Some game have a multiplayer mode
        public APreload.MultiplayerType multiplayerType; // How multiplayer is handled
    }

    public enum Difficulty // Easy mode give twice more time
    {
        Normal = 1,
        Easy
    }
}
