using System;

namespace SU_Casino
{
    public class helpClass
    {
    }
    public class Playerlog
    {
        public string userid { get; set; }
        public string condition { get; set; }
        public string gamename { get; set; }
        public int moment { get; set; }
        public int trial { get; set; }
        public int balance_in { get; set; }
        public int balance_out { get; set; }
        public string stimuli { get; set; }
        public int bet { get; set; }
        public int outcome { get; set; }
        public string response { get; set; }
        public DateTime timestamp_begin { get; set; }
        public DateTime timestamp_R { get; set; }
        public DateTime timestamp_O { get; set; }
        public string questionForWinChance { get; set; }
        public string figure1 { get; set; }
        public string figure2 { get; set; }
        public string figure3 { get; set; }
    }

    public class EventLog
    {
        public string userid { get; set; }
        public string title { get; set; }
        public string message { get; set; }

        public EventLog(string title, string userid, Exception ex)
        {
            this.title = title;
            this.message = ex.Message;
            this.userid = userid;
        }
    }
}