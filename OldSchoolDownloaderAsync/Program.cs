// Program.cs

namespace ProAsyncNet.OldSchoolDownloaderAsync
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            string url = "http://www.rocksolidknowledge.com/";

            Console.WriteLine("Downloading web page ...");

            try
            {
                var task = DownloadWebPageAsync(url);

                while (!task.IsCompleted)
                {
                    Console.Write(".");
                    Thread.Sleep(100);
                }

                Console.WriteLine();
                Console.WriteLine("Done!");
                Console.WriteLine();

                Console.Write("Show result? (y/n) ");
                ConsoleKeyInfo key = Console.ReadKey(false);
                Console.WriteLine();
                Console.WriteLine();

                if (key.KeyChar.ToString().Equals("y", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine(task.Result);
                }
            }
            catch (Exception ex)
            {
                var nl = Environment.NewLine;

                Console.WriteLine();
                Console.WriteLine($"Error: {ex.Message} {ex.InnerException?.Message}{nl}{nl}{ex.StackTrace}{ex.InnerException?.StackTrace}");
            }

            Console.WriteLine();
        }

        private static string DownloadWebPage(string url)
        {
            var webRequest = WebRequest.Create(url);

            using (var webResponse = webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        private static Task<string> DownloadWebPageAsync(string url)
            => Task.Factory.StartNew(() => DownloadWebPage(url));

        private static Task<string> BetterDownloadWebPageAsync(string url)
        {
            var webRequest = WebRequest.Create(url);

            var asyncResult = webRequest.BeginGetResponse(null, null);

            var task = Task.Factory.FromAsync<string>(
                asyncResult,
                ar =>
                {
                    using (var response = webRequest.EndGetResponse(ar))
                    {
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                });

            return task;
        }
    }
}
