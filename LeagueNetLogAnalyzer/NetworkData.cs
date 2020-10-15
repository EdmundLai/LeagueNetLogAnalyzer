using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueNetLogAnalyzer
{
    class NetworkData
    {
        /// <summary>
        /// time = time since start of game in ms.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// address = address of IP address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// incoming = total size of messages set to me in bytes. Spikes can indicate lag.
        /// </summary>
        public int Incoming { get; set; }

        /// <summary>
        /// outgoing = total size of messages set from me in bytes. Spikes can indicate lag.
        /// </summary>
        public int Outgoing { get; set; }

        /// <summary>
        /// app_ctos = client to server bytes delta excluding overheads (app data only)
        /// </summary>
        /// 
        public int AppCtos { get; set; }

        /// <summary>
        /// app_stoc = server to client bytes delta excluding overheads (app data only)
        /// </summary>
        public int AppStoc { get; set; }

        /// <summary>
        /// loss = number of packets lost. Can indicate lags.  Some node on the network is dropping packets or taking to long and we have to resend them.
        /// </summary>
        public int Loss { get; set; }

        /// <summary>
        /// sent = number of packets sent. Spikes can indicate lag.
        /// </summary>
        public int Sent { get; set; }

        /// <summary>
        /// ping = how long it takes for a packet to get back and forth. Spikes can indicate lag.
        /// </summary>
        public int Ping { get; set; }

        /// <summary>
        /// variance = how much the ping is changing from avg ping. Spikes can indicate lag.
        /// </summary>
        public int Variance { get; set; }

        /// <summary>
        /// reliable delayed = This occurs when we drop reliable packets from sending this frame because we are sending to much data. Unreliable are more likely to be dropped than reliable.
        /// </summary>
        public int ReliableDelayed { get; set; }

        /// <summary>
        /// unreliable delayed = This occurs when we drop unreliable packets from sending this frame because we are sending too much data.
        /// </summary>
        public int UnreliableDelayed { get; set; }

        /// <summary>
        /// app update delayed = This occurs when the app is taking to long to run code caused by network packets (ie CPU) and packets are delayed until the next frame
        /// </summary>
        public int AppUpdateDelayed { get; set; }

        /// <summary>
        /// Time spent in critical section (frame) = This is the time the network thread is blocking the main thread from doing anything.
        /// </summary>
        public int CritSectionTime { get; set; }

        /// <summary>
        /// Latency in Window = The average Latency in ms. of Reliable packets during this time slice
        /// </summary>
        public int LatencyInWindow { get; set; }

        /// <summary>
        /// Packet Loss percentage in Window = The packet loss percentage of Reliable packets during this time slice
        /// </summary>
        public double PacketLossPercentageInWindow { get; set; }

        /// <summary>
        /// Jitter in Window = The average Jitter in ms. of Reliable packets during this time slice
        /// </summary>
        public int JitterInWindow { get; set; }

        /// <summary>
        /// Overall Latency Min = The lowest Latency seen in ms. of Reliable packets up to this point in time
        /// </summary>
        public int OverallLatencyMin { get; set; }

        /// <summary>
        /// Overall Latency Max = The largest Latency seen in ms. of Reliable packets up to this point in time
        /// </summary>
        public int OverallLatencyMax { get; set; }

        /// <summary>
        /// Latency Min in Window = The lowest Latency seen in ms. of Reliable packets during this time slice
        /// </summary>
        public int LatencyMinInWindow { get; set; }

        /// <summary>
        /// Latency Max in Window = The largest Latency seen in ms. of Reliable packets during this time slice
        /// </summary>
        public int LatencyMaxInWindow { get; set; }

        /// <summary>
        /// Latency packet samples = number of packets used in determining average latency from start of game
        /// </summary>
        public int LatencyPacketSamples { get; set; }

        /// <summary>
        /// Jitter packet samples = number of packets used in determining average jitter from start of game
        /// </summary>
        public int JitterPacketSamples { get; set; }

        /// <summary>
        /// reconnect = flag to indicate one or more reconnects occurred
        /// </summary>
        public int Reconnect { get; set; }


    }
}
