﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SanaraV2.Features.Tools
{
    public static class Communication
    {
        public static async Task<FeatureRequest<Response.Complete, Error.Complete>> Complete(string[] args, Action<string> onReceived, Action<string> onError, Action onClose)
        {
            if (args.Length == 0)
                return new FeatureRequest<Response.Complete, Error.Complete>(null, Error.Complete.Help);
            var ws = new WebSocket("ws://163.172.76.10:8080");
            ws.Origin = "http://textsynth.org";

            ws.OnMessage += (sender, e)
                => onReceived(e.Data);

            ws.OnError += (sender, e)
                => onError(e.Message);

            ws.OnClose += (sender, e)
                => onClose();

            ws.Connect();
            ws.Send("g," + string.Join(" ", args));

            return new FeatureRequest<Response.Complete, Error.Complete>(new Response.Complete { }, Error.Complete.None);
        }

        public static async Task<FeatureRequest<Response.Inspire, Error.None>> Inspire()
        {
            using (HttpClient hc = new HttpClient())
                return new FeatureRequest<Response.Inspire, Error.None>(new Response.Inspire { url = await hc.GetStringAsync("https://inspirobot.me/api?generate=true") }, Error.None.None);
        }
    }
}
