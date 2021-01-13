using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Json.Net;
using Newtonsoft.Json;

public class MyFirstModule : BaseCommandModule
{
    
    [Command("VTscan")]
    public async Task GreetCommand(CommandContext ctx, string name)
    {

        await ctx.RespondAsync("got it. Querying server for hash `" + name + "`");
       // await ctx.RespondAsync("send cake");

        using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
        {
            client.BaseAddress = new Uri("https://www.virustotal.com/vtapi/v2/file/");
            HttpResponseMessage response = client.GetAsync("report?apikey=" + Dsharp.info.tokens.VT_token + "&resource=" + name).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Result: " + result);
            
            //now we need to parse the result
            try
            {
                dynamic scanresult = JsonConvert.DeserializeObject(result);
                string positive = scanresult.positives;
                string total = scanresult.total;
                string URL = scanresult.permalink;
                await ctx.RespondAsync("Result: " + positive + "/" + total);
                await ctx.RespondAsync(URL);

                if(Convert.ToInt32(positive) > 2)
                {
                    bool Kaspersky = scanresult.scans.Kaspersky.detected;
                    bool Bitdefender = scanresult.scans.BitDefender.detected;
                    bool Sophos = scanresult.scans.Sophos.detected;
                    bool Microsoft = scanresult.scans.Microsoft.detected;

                    await ctx.RespondAsync("Detected by Kaspersky: " + Kaspersky);
                    await ctx.RespondAsync("Detected by Bitdefender: " + Bitdefender);
                    await ctx.RespondAsync("Detected by Sophos: " + Sophos);
                    await ctx.RespondAsync("Detected by Microsoft: " + Microsoft);


                }
            }
            catch(Exception errorrr)
            {
                await ctx.RespondAsync("Error occured. sowwy OwO");
                Console.WriteLine(errorrr.Message);
            }
            
            //await ctx.RespondAsync(result);
        }
        
      
    }

    [Command("VTembed")]
    public async Task Greetembed(CommandContext ctx, string name)
    {
        string hashtouse = "";
        try
        {
             hashtouse = getBetween(name, "https://www.virustotal.com/gui/file/", "/detection");
        }catch (Exception ohno)
        {
            await ctx.RespondAsync("Invalid URL or URL in wrong format.");
        }
        await ctx.RespondAsync("got it. Querying server for hash `" + hashtouse + "`");
        // await ctx.RespondAsync("send cake");

        using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
        {
            client.BaseAddress = new Uri("https://www.virustotal.com/vtapi/v2/file/");
            HttpResponseMessage response = client.GetAsync("report?apikey=" + Dsharp.info.tokens.VT_token + "&resource=" + hashtouse).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Result: " + result);

            //now we need to parse the result
            try
            {
                dynamic scanresult = JsonConvert.DeserializeObject(result);
                string positive = scanresult.positives;
                string total = scanresult.total;
                string URL = scanresult.permalink;
                await ctx.RespondAsync("Result: " + positive + "/" + total);
               // await ctx.RespondAsync(URL);

                if (Convert.ToInt32(positive) > 2)
                {
                    bool Kaspersky = scanresult.scans.Kaspersky.detected;
                    bool Bitdefender = scanresult.scans.BitDefender.detected;
                    bool Sophos = scanresult.scans.Sophos.detected;
                    bool Microsoft = scanresult.scans.Microsoft.detected;

                    await ctx.RespondAsync("Detected by Kaspersky: " + Kaspersky);
                    await ctx.RespondAsync("Detected by Bitdefender: " + Bitdefender);
                    await ctx.RespondAsync("Detected by Sophos: " + Sophos);
                    await ctx.RespondAsync("Detected by Microsoft: " + Microsoft);


                }
            }
            catch (Exception errorrr)
            {
                await ctx.RespondAsync("Error occured. sowwy OwO");
                Console.WriteLine(errorrr.Message);
            }

            //await ctx.RespondAsync(result);
        }

    }
    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }

        return "";
    }
}
