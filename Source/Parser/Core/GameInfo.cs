using System;

namespace Parser.Core
{
    [Serializable]
    public class GameInfo
    {
        private static int global_id_gen;
        public int ID { get; set; }
        public DateTime Date  { get; set; }
        public DateTime Time { get; set; }
        public string Url { get; set; }
        public string GameName { get; set; }
        public string Server { get; set; }
        public string Category { get; set; }
        public string ServerName { get; set; }
        public string Turn { get; set; }
        public long CoinsCount { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public bool IsOnline { get; set; }
        public string Nick { get; set; }
        public int ReviewsCount { get; set; }
        public string RegisteredTime { get; set; }
        public string PriceTitle { get; set; }
        public GameInfo(DateTime date, DateTime time, string url, string gameName, string server,
                        string category, string serverName, string turn, long coinsCount, double cost, 
                        string description, string nick, int reviewsCount, string registeredTime, string priceTitle,
                        bool isOnline)
        {
            ID = ++global_id_gen;
            Date = date;
            Time = time;
            Url = url;
            GameName = gameName;
            Server = server;
            ServerName = serverName;
            Category = category;
            Turn = turn;
            CoinsCount = coinsCount;
            Cost = cost;
            Description = description;
            IsOnline = isOnline;
            Nick = nick;
            ReviewsCount = reviewsCount;
            RegisteredTime = registeredTime;
            PriceTitle = priceTitle;
        }

        public GameInfo()
        {

        }
    }
}
