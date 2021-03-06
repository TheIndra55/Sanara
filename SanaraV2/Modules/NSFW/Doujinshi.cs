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
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SanaraV2.Modules.NSFW
{
    public class Doujinshi : ModuleBase
    {
        Program p = Program.p;


        [Command("Subscribe Doujinshi"), Alias("Subscribe doujin", "Subscribe nhentai")]
        public async Task Subscribe(params string[] args)
        {
            if (Context.Guild == null)
            {
                await ReplyAsync(Base.Sentences.CommandDontPm(Context.Guild));
                return;
            }
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            if (!Tools.Settings.CanModify(Context.User, Context.Guild))
            {
                await ReplyAsync(Base.Sentences.OnlyOwnerStr(Context.Guild, Context.Guild.OwnerId));
            }
            else
            {
                var result = await Features.NSFW.Doujinshi.Subscribe(Context.Guild, Program.p.db, args, !((ITextChannel)Context.Channel).IsNsfw);
                switch (result.error)
                {
                    case Features.NSFW.Error.Subscribe.ChanNotSafe:
                        await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                        break;

                    case Features.NSFW.Error.Subscribe.DestChanNotSafe:
                        await ReplyAsync(Sentences.SubscribeSafeDestination(Context.Guild));
                        break;

                    case Features.NSFW.Error.Subscribe.Help:
                        await ReplyAsync(Sentences.SubscribeNHentaiHelp(Context.Guild));
                        break;

                    case Features.NSFW.Error.Subscribe.InvalidChannel:
                        await ReplyAsync(Entertainment.Sentences.InvalidChannel(Context.Guild));
                        break;

                    case Features.NSFW.Error.Subscribe.None:
                        await ReplyAsync(Entertainment.Sentences.SubscribeDone(Context.Guild, "doujinshi", result.answer.chan) + Environment.NewLine
                            + Sentences.Blacklist(Context.Guild) + result.answer.subscription.GetBlacklistTags() + Environment.NewLine
                            + Sentences.Whitelist(Context.Guild) + result.answer.subscription.GetWhitelistTags());
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        [Command("Unsubscribe Doujinshi"), Alias("Unsubscribe doujin", "Unsubscribe nhentai")]
        public async Task Unsubcribe(params string[] args)
        {
            if (Context.Guild == null)
            {
                await ReplyAsync(Base.Sentences.CommandDontPm(Context.Guild));
                return;
            }
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            if (!Tools.Settings.CanModify(Context.User, Context.Guild))
            {
                await ReplyAsync(Base.Sentences.OnlyOwnerStr(Context.Guild, Context.Guild.OwnerId));
            }
            else
            {
                var result = await Features.NSFW.Doujinshi.Unsubscribe(Context.Guild, Program.p.db, !((ITextChannel)Context.Channel).IsNsfw);
                switch (result.error)
                {
                    case Features.NSFW.Error.Unsubscribe.NoSubscription:
                        await ReplyAsync(Entertainment.Sentences.NoSubscription(Context.Guild));
                        break;

                    case Features.NSFW.Error.Unsubscribe.None:
                        await ReplyAsync(Entertainment.Sentences.UnsubscribeDone(Context.Guild, "doujinshi"));
                        break;

                    case Features.NSFW.Error.Unsubscribe.ChanNotSafe:
                        await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
        }


        [Command("Download doujinshi", RunMode = RunMode.Async)]
        public async Task GetDownloadDoujinshi(params string[] args)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            IMessage msg = null;
            var textChan = Context.Channel as ITextChannel;
            var result = await Features.NSFW.Doujinshi.SearchDownloadDoujinshi(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false, args, async () =>
            {
                msg = await ReplyAsync("Preparing download, this might take some time...");
            });
            switch (result.error)
            {
                case Features.NSFW.Error.Download.ChanNotSafe:
                    await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                    break;

                case Features.NSFW.Error.Download.Help:
                    await ReplyAsync(Sentences.DownloadDoujinshiHelp(Context.Guild));
                    break;

                case Features.NSFW.Error.Download.NotFound:
                    await ReplyAsync(Sentences.DownloadDoujinshiNotFound(Context.Guild));
                    break;

                case Features.NSFW.Error.Download.None:
                    await GetDownloadResult(msg, result.answer);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        [Command("Download cosplay", RunMode = RunMode.Async)]
        public async Task GetDownloadCosplay(params string[] args)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            IMessage msg = null;
            var result = await Features.NSFW.Doujinshi.SearchDownloadCosplay(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false, args, async () =>
            {
                msg = await ReplyAsync("Preparing download, this might take some time...");
            });
            switch (result.error)
            {
                case Features.NSFW.Error.Download.ChanNotSafe:
                    await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                    break;

                case Features.NSFW.Error.Download.Help:
                    await ReplyAsync(Sentences.DownloadCosplayHelp(Context.Guild));
                    break;

                case Features.NSFW.Error.Download.NotFound:
                    await ReplyAsync(Sentences.DownloadCosplayNotFound(Context.Guild));
                    break;

                case Features.NSFW.Error.Download.None:
                    await GetDownloadResult(msg, result.answer);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        [Command("Download"), Priority(-1)]
        public async Task GetDownloadDefault(params string[] _)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            await ReplyAsync(Sentences.DownloadHelp(Context.Guild));
        }

        public async Task GetDownloadResult(IMessage msg, Features.NSFW.Response.Download answer)
        {
            FileInfo fi = new FileInfo(answer.filePath);
            if (fi.Length < 8000000)
                await Context.Channel.SendFileAsync(answer.filePath);
            else
            {
                if (Program.p.websiteUpload == null)
                    throw new NullReferenceException("File bigger than 8MB and websiteUpload key null");
                else
                {
                    string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Directory.CreateDirectory(Program.p.websiteUpload + "/" + now);
                    File.Copy(answer.filePath, Program.p.websiteUpload + "/" + now + "/" + answer.id + ".zip");
                    await ReplyAsync(Program.p.websiteUrl + "/" + now + "/" + answer.id + ".zip" + Environment.NewLine + Sentences.DeleteTime(Context.Guild, "10"));
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(600000); // 10 minutes
                        File.Delete(Program.p.websiteUpload + "/" + now + "/" + answer.id + ".zip");
                        Directory.Delete(Program.p.websiteUpload + "/" + now);
                    });
                }
            }
            await msg.DeleteAsync();
            File.Delete(answer.filePath);
            Directory.Delete(answer.directoryPath);
        }

        [Command("AdultVideo", RunMode = RunMode.Async), Alias("AV")]
        public async Task GetAdultVideo(params string[] args)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            if (Program.p.categories.Count == 2) // Tags not loaded
            {
                await ReplyAsync("", false, new EmbedBuilder {
                    Description = "An error occurred while loading tags, the command is therefore temporarily unavailable.",
                    Color = Color.Red
                }.Build());
                return;
            }
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            var result = await Features.NSFW.Doujinshi.SearchAdultVideo(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false, args, Program.p.rand, Program.p.categories);
            switch (result.error)
            {
                case Features.NSFW.Error.Doujinshi.ChanNotNSFW:
                    await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                    break;

                case Features.NSFW.Error.Doujinshi.NotFound:
                    await ReplyAsync(Base.Sentences.TagsNotFound(Context.Guild, args));
                    break;

                case Features.NSFW.Error.Doujinshi.None:
                    await ReplyAsync("", false, CreateFinalEmbed(result.answer, null));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        [Command("Cosplay", RunMode = RunMode.Async)]
        public async Task GetCosplay(params string[] args)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            var result = await Features.NSFW.Doujinshi.SearchCosplay(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false, args, Program.p.rand);
            switch (result.error)
            {
                case Features.NSFW.Error.Doujinshi.ChanNotNSFW:
                    await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                    break;

                case Features.NSFW.Error.Doujinshi.NotFound:
                    await ReplyAsync(Base.Sentences.TagsNotFound(Context.Guild, args));
                    break;

                case Features.NSFW.Error.Doujinshi.None:
                    await ReplyAsync("", false, CreateFinalEmbed(result.answer, Sentences.DownloadCosplayInfo));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        [Command("Doujinshi", RunMode = RunMode.Async), Priority(-1), Summary("Give a random doujinshi using nhentai API")]
        public async Task GetNhentai(params string[] keywords)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            var result = await Features.NSFW.Doujinshi.SearchDoujinshi(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false, keywords, Program.p.rand);
            switch (result.error)
            {
                case Features.NSFW.Error.Doujinshi.ChanNotNSFW:
                    await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                    break;

                case Features.NSFW.Error.Doujinshi.NotFound:
                    await ReplyAsync(Base.Sentences.TagsNotFound(Context.Guild, keywords));
                    break;

                case Features.NSFW.Error.Doujinshi.None:
                    await ReplyAsync("", false, CreateFinalEmbed(result.answer, Sentences.DownloadDoujinshiInfo));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        [Command("Doujinshi popularity", RunMode = RunMode.Async), Alias("Doujinshi p")]
        public async Task GetNhentaiPopularity(params string[] keywords)
        {
            Base.Utilities.CheckAvailability(Context.Guild, Program.Module.Doujinshi);
            await p.DoAction(Context.User, Program.Module.Doujinshi);
            var result = keywords.Length == 0
                ? await Features.NSFW.Doujinshi.SearchDoujinshiRecentPopularity(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false)
                : await Features.NSFW.Doujinshi.SearchDoujinshiByPopularity(Context.Channel is ITextChannel ? !((ITextChannel)Context.Channel).IsNsfw : false, keywords);
            switch (result.error)
            {
                case Features.NSFW.Error.Doujinshi.ChanNotNSFW:
                    await ReplyAsync(Base.Sentences.ChanIsNotNsfw(Context.Guild));
                    break;

                case Features.NSFW.Error.Doujinshi.NotFound:
                    await ReplyAsync(Base.Sentences.TagsNotFound(Context.Guild, keywords));
                    break;

                case Features.NSFW.Error.Doujinshi.None:
                    var embed = new EmbedBuilder
                    {
                        Title = keywords.Length == 0 ?  "Recently popular doujinshi" : ("Most popular doujinshi with the tag" + (keywords.Length > 1 ? "s" : "") + " " + string.Join(", ", keywords)),
                        ImageUrl = result.answer[0].imageUrl,
                        Color = new Color(255, 20, 147),
                        Footer = new EmbedFooterBuilder
                        {
                            Text = "#1: " +  result.answer[0].title + "\nTags: " + string.Join(", ", result.answer[0].tags)
                        }
                    };
                    int i = 1;
                    List<Tuple<string, string, string>> doujinInfos = new List<Tuple<string, string, string>>();
                    foreach (var elem in result.answer)
                    {
                        embed.AddField("#" + i + ": " + elem.title, elem.url);
                        doujinInfos.Add(new Tuple<string, string, string>(elem.title, string.Join(", ", elem.tags), elem.imageUrl));
                        i++;
                    }
                    var msg = await ReplyAsync("", false, embed.Build());
                    await msg.AddReactionsAsync(new[] { new Emoji("◀️"), new Emoji("▶️") });
                    Program.p.DOUJINSHI_POPULARITY_INFO.Add(msg.Id, new Tuple<int, Tuple<string, string, string>[]>(0, doujinInfos.ToArray()));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public Embed CreateFinalEmbed(Features.NSFW.Response.Doujinshi result, Func<IGuild, string, string> downloadInfo)
        {
            return new EmbedBuilder()
            {
                Color = new Color(255, 20, 147),
                Description = string.Join(", ", result.tags),
                Title = result.title,
                Url = result.url,
                ImageUrl = result.imageUrl,
                Footer = new EmbedFooterBuilder()
                {
                    Text = Sentences.ClickFull(Context.Guild) + (downloadInfo == null ? "" : "\n\n" + downloadInfo(Context.Guild, result.id.ToString()))
                }
            }.Build();
        }
    }
}