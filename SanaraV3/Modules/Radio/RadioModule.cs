﻿using Discord;
using Discord.Audio;
using Discord.Commands;
using DiscordUtils;
using SanaraV3.Exceptions;
using System.IO;
using System.Threading.Tasks;

namespace SanaraV3.Modules.Radio
{
    public sealed class RadioModule : ModuleBase, IModule
    {
        public string GetModuleName()
            => "Radio";

        [Command("Skip radio", RunMode = RunMode.Async), Alias("Radio skip")]
        public async Task Skip()
        {
            if (!StaticObjects.Radios.ContainsKey(Context.Guild.Id))
                await ReplyAsync("There is no radio running.");
            else
                StaticObjects.Radios[Context.Guild.Id].Skip();
        }

        [Command("Playlist radio"), Alias("Radio playlist")]
        public async Task Playlist()
        {
            if (!StaticObjects.Radios.ContainsKey(Context.Guild.Id))
                await ReplyAsync("There is no radio running.");
            else
                await ReplyAsync(StaticObjects.Radios[Context.Guild.Id].GetPlaylist());
        }

        [Command("Stop radio"), Alias("Radio stop")]
        public async Task Stop()
        {
            if (!StaticObjects.Radios.ContainsKey(Context.Guild.Id))
                await ReplyAsync("There is no radio running.");
            else
            {
                await StaticObjects.Radios[Context.Guild.Id].StopAsync();
                StaticObjects.Radios.Remove(Context.Guild.Id);
            }
        }

        [Command("Remove radio", RunMode = RunMode.Async), Alias("Radio remove")]
        public async Task Remove([Remainder]string search)
        {
            if (!StaticObjects.Radios.ContainsKey(Context.Guild.Id))
                await ReplyAsync("There is no radio running.");
            else
            {
                StaticObjects.Radios[Context.Guild.Id].RemoveMusic(search);
                await ReplyAsync("Done~");
            }
        }

        [Command("Remove radio", RunMode = RunMode.Async), Alias("Radio remove"), Priority(1)]
        public async Task Remove(int id)
        {
            if (!StaticObjects.Radios.ContainsKey(Context.Guild.Id))
                await ReplyAsync("There is no radio running.");
            else
            {
                StaticObjects.Radios[Context.Guild.Id].RemoveMusicWithId(id);
                await ReplyAsync("Done~");
            }
        }

        [Command("Add radio", RunMode = RunMode.Async), Alias("Radio add")]
        public async Task Add([Remainder]string search)
        {
            var radio = StaticObjects.Radios.ContainsKey(Context.Guild.Id) ? StaticObjects.Radios[Context.Guild.Id] : null;
            if (radio != null && !radio.CanAddMusic())
                throw new CommandFailed("You can't add more musics to the playlist.");

            if (radio == null)
                radio = await StartAsync();
            var video = await Entertainment.MediaModule.GetYoutubeVideoAsync(search);
            string url = "https://youtu.be/" + video.Id;
            if (radio.HaveMusic(url))
                throw new CommandFailed("This music is already in the playlist.");
            await ReplyAsync(video.Snippet.Title + " was added to the playlist.");
            if (!Directory.Exists($"Saves/Radio/{Context.Guild.Id}"))
                Directory.CreateDirectory($"Saves/Radio/{Context.Guild.Id}");
            if (radio.IsLastMusicSuggestion())
            {
                File.Delete($"Saves/Radio/{Context.Guild.Id}/{Utils.CleanWord(video.Snippet.Title)}suggestion.mp3");
                radio.RemoveLastMusic();
            }
            radio.DownloadMusic(Context.Guild.Id, video, Context.User.ToString(), false);
            await radio.Play();
        }

        [Command("Start radio"), Alias("Radio start", "Launch radio", "Radio launch")]
        public async Task Start()
        {
            await StartAsync();
        }

        private async Task<RadioChannel> StartAsync()
        {
            if (StaticObjects.Radios.ContainsKey(Context.Guild.Id))
                throw new CommandFailed("You radio is already on.");
            IGuildUser guildUser = Context.User as IGuildUser;
            if (guildUser.VoiceChannel == null)
                throw new CommandFailed("You must be in a vocal channel to use this command.");
            IAudioClient audioClient = await guildUser.VoiceChannel.ConnectAsync();
            RadioChannel radio = new RadioChannel(guildUser.VoiceChannel, Context.Channel, audioClient);
            StaticObjects.Radios.Add(guildUser.GuildId, radio);
            return radio;
        }
    }
}