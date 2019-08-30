using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Data.SQLite;

namespace KavLachayimAPI
{
    public class App
    {
        public static string DBFilePath = Environment.CurrentDirectory + @"\Data\AccountsDB.db3";
        public static string WebRootPath;
        public static SQLiteConnection AccountsDatabaseConnection = new SQLiteConnection("Data Source=" + DBFilePath + ";Version=3");

        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel().
                 UseUrls("http://*:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
            host.Run();
        }
    }
}
