﻿using Discord;
using DiscordUtils;
using SanaraV3.Subscription.Impl;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanaraV3.Subscription
{
    public sealed class SubscriptionManager
    {
        public SubscriptionManager()
        {
            _subscriptions = new ISubscription[]
            {
                new NHentaiSubscription(),
                new AnimeSubscription()
            };
        }

        public async Task InitAsync()
        {
            // Init all subscriptions
            foreach (var sub in _subscriptions)
                StaticObjects.Db.InitSubscription(sub.GetName());

            // We set the current value on the subscription // TODO: Maybe we shouldn't reset things everytimes the bot start
            foreach (var sub in _subscriptions)
            {
                var feed = await sub.GetFeedAsync(StaticObjects.Db.GetCurrent(sub.GetName()));
                if (feed.Length > 0)
                    await StaticObjects.Db.SetCurrentAsync(sub.GetName(), feed[0].Id); // Somehow doing the GetCurrent inside the GetFeedAsync stuck the bot
            }

            // Subscription loop
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(600000); // We check for new content every 10 minutes
                    await Update();
                }
            });
        }

        public async Task<Dictionary<string, ITextChannel>> GetSubscriptionsAsync(ulong guildId)
        {
            var d = new Dictionary<string, ITextChannel>();
            foreach (var sub in _subscriptions)
            {
                string name = sub.GetName();
                d.Add(name, await StaticObjects.Db.HasSubscriptionExistAsync(guildId, name) ? StaticObjects.Db.GetAllSubscriptions(name).Where(x => x.TextChan.GuildId == guildId).First().TextChan : null);
            }
            return d;
        }

        public Dictionary<string, int> GetSubscriptionCount()
        {
            var d = new Dictionary<string, int>();
            foreach (var sub in _subscriptions)
            {
                string name = sub.GetName();
                d.Add(name, StaticObjects.Db.GetAllSubscriptions(name).Length);
            }
            return d;
        }

        private async Task Update()
        {
            foreach (var sub in _subscriptions)
            {
                try
                {
                    var feed = await sub.GetFeedAsync(StaticObjects.Db.GetCurrent(sub.GetName()));
                    if (feed.Length > 0) // If there is anything new in the feed compared to last time
                    {
                        await StaticObjects.Db.SetCurrentAsync(sub.GetName(), feed[0].Id);
                        foreach (var elem in StaticObjects.Db.GetAllSubscriptions(sub.GetName()))
                        {
                            try
                            {
                                foreach (var data in feed)
                                {
                                    if (elem.Tags.IsTagValid(data.Tags)) // Check if tags are valid with black/whitelist
                                    {
                                        await elem.TextChan.SendMessageAsync(embed: data.Embed);
                                    }
                                }
                            }
                            catch (System.Exception e)
                            {
                                await Log.ErrorAsync(new LogMessage(LogSeverity.Error, e.Source, e.Message, e));
                            }
                        }
                    }
                }
                catch (System.Exception e) // If somehow wrong happens while getting new subscription
                {
                    await Log.ErrorAsync(new LogMessage(LogSeverity.Error, e.Source, e.Message, e));
                }
            }
        }

        private ISubscription[] _subscriptions;
    }
}
