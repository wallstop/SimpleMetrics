using System.Data.SQLite;

namespace SimpleMetrics
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SQLiteConnection.CreateFile("MetricsDB.sqlite");
        }
    }
}