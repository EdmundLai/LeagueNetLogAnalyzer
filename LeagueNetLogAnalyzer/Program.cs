using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LeagueNetLogAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            //string testFolder = @"C:\Riot Games\League of Legends\Logs\GameLogs\2020-10-12T23-00-17";
            //AnalyzeNetworkData(GetNetLogContents(testFolder));
            AnalyzeLatestNetLogData();
            
        }

        static void AnalyzeLatestNetLogData()
        {
            string[] netLogFolders = System.IO.Directory.GetDirectories(@"C:\Riot Games\League of Legends\Logs\GameLogs");
            DateTime latestDate = new DateTime(0);
            string latestFolder = "";
            foreach (string folder in netLogFolders)
            {
                //Console.WriteLine(folder);
                //string[] folderFiles = Directory.GetFiles(folder);

                string lastDirectory = new DirectoryInfo(folder).Name;

                //Console.WriteLine(lastDirectory);


                DateTime gameDate = DateTime.ParseExact(lastDirectory, "yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
                //Console.WriteLine(gameDate.ToString());

                if(gameDate > latestDate)
                {
                    latestDate = gameDate;
                    latestFolder = folder;
                }

                //Console.WriteLine(gameDate.ToString("yyyy-MM-dd"));
            }

            if (!String.IsNullOrEmpty(latestFolder))
            {
                Console.WriteLine($"Ping data from latest game:");
                AnalyzeNetworkData(GetNetLogContents(latestFolder));
            } else
            {
                Console.WriteLine("No network data available to analyze!");
            }
            
        }


        static List<NetworkData> GetNetLogContents(string folder)
        {
            string lastDirectory = new DirectoryInfo(folder).Name;

            //Console.WriteLine(lastDirectory);

            DateTime gameDate = DateTime.ParseExact(lastDirectory, "yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
            //Console.WriteLine(gameDate.ToString());
            DateTime gameDateTmr = gameDate.AddDays(1);

            // possible prefixes to non-data portions of log
            string[] prefixes = { gameDate.ToString("yyyy-MM-dd"), gameDateTmr.ToString("yyyy-MM-dd") };

            // path to netlog file
            string netlogPath = Path.Combine(folder, $"{lastDirectory}_netlog.txt");

            //Console.WriteLine(netlogPath);

            string[] netlogContents = System.IO.File.ReadAllLines(netlogPath);

            List<string> netlogData = FilterNetLogContents(netlogContents, prefixes);

            List<NetworkData> networkDatas = new List<NetworkData>();

            // it works! :)
            foreach(var line in netlogData)
            {
                //Console.WriteLine(line);
                networkDatas.Add(ConvertLineToNetworkData(line));
            }

            //Console.WriteLine($"number of network data received: {networkDatas.Count}");

            return networkDatas;
        }

        static void AnalyzeNetworkData(List<NetworkData> networkDatas)
        {
            foreach(var data in networkDatas)
            {
                double numGameMin = Math.Round(((double) data.Time / 60000), 2);
                Console.WriteLine($"Ping at {numGameMin} minutes: {data.Ping} ms");
            }
        }

        static NetworkData ConvertLineToNetworkData(string line)
        {
            //Console.WriteLine(line);
            string[] lineData = line.Split(",");

            double packetLossPercent;
            if(!double.TryParse(lineData[15], out packetLossPercent))
            {
                //Console.WriteLine($"{lineData[15]} was not a double.");
                packetLossPercent = 0;
            }

            var networkData = new NetworkData
            {
                Time = Int32.Parse(lineData[0]),
                Address = lineData[1],
                Incoming = Int32.Parse(lineData[2]),
                Outgoing = Int32.Parse(lineData[3]),
                AppCtos = Int32.Parse(lineData[4]),
                AppStoc = Int32.Parse(lineData[5]),
                Loss = Int32.Parse(lineData[6]),
                Sent = Int32.Parse(lineData[7]),
                Ping = Int32.Parse(lineData[8]),
                Variance = Int32.Parse(lineData[9]),
                ReliableDelayed = Int32.Parse(lineData[10]),
                UnreliableDelayed = Int32.Parse(lineData[11]),
                AppUpdateDelayed = Int32.Parse(lineData[12]),
                CritSectionTime = Int32.Parse(lineData[13]),
                LatencyInWindow = Int32.Parse(lineData[14]),
                PacketLossPercentageInWindow = packetLossPercent,
                JitterInWindow = Int32.Parse(lineData[16]),
                OverallLatencyMin = Int32.Parse(lineData[17]),
                OverallLatencyMax = Int32.Parse(lineData[18]),
                LatencyMinInWindow = Int32.Parse(lineData[19]),
                LatencyMaxInWindow = Int32.Parse(lineData[20]),
                LatencyPacketSamples = Int32.Parse(lineData[21]),
                JitterPacketSamples = Int32.Parse(lineData[22]),
                Reconnect = Int32.Parse(lineData[23]),
            };

            return networkData;
        }

        // filters netlog of any extraneous text data meant for humans
        static List<string> FilterNetLogContents(string[] netlogContents, string[] prefixes)
        {
            return netlogContents.Where(line => !prefixes.Any(prefix => line.StartsWith(prefix))).ToList();
        }
    }
}
