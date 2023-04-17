using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    // WhenAll'dan farkı, WhenAll ana threadi bloklamazken, WaitAll' da ana thread bloklanıyor.Ms cinsinden parametre alıyor ayrıca WhenAll'dan farklı olarak.
    public class WaitAll
    {
        public static async Task<Content> GetContentAsync(string url)
        {
            Content c = new();
            var data = await new HttpClient().GetStringAsync(url);

            c.Site = url;
            c.Length = data.Length;
            Console.WriteLine($"GetContentAsync thread: {Thread.CurrentThread.ManagedThreadId}");

            return c;
        }
        public class Content
        {
            public string? Site { get; set; }
            public int Length { get; set; }
        }

        public async static Task WaitAllTask()
        {
            Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");

            List<string> urlList = new()
            {
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com"
            };

            List<Task<Content>> tasks = new();

            urlList.ToList().ForEach(url =>
            {
                tasks.Add(GetContentAsync(url));
            });

            Console.WriteLine("Before WaitAll");

            // ilgili urlList içerisindeki 3 data 3sn'de gelirse isFinishedTime=true, 3sn'de gelmezse isFinishedTime=false olarak dönecektir.
            bool isFinishedTime = Task.WaitAll(tasks.ToArray(), 1300);
            Console.WriteLine($"Is Finished Time: {isFinishedTime}");

            Console.WriteLine("After WaitAll");

            Console.WriteLine($"{tasks.First().Result.Site} - {tasks.First().Result.Length}");
        }
    }
}
