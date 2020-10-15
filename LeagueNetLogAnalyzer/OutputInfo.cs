using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueNetLogAnalyzer
{
    class OutputInfo
    {
        public DateTime GameDate { get; set; }
        public List<PingData> PingDatas { get; set; }
    }
}
