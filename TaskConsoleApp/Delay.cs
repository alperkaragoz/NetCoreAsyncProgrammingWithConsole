using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    // Asenkron olarak gecikme gerçekleştirir.Bu gecikme gerçekleşirken güncel thread i bloklamaz.Thread'in Sleep metodu ana threadi bloklar.
    public class Delay
    {
        public static async Task<Content> GetContentAsync(string url)
        {
            Content c = new();
            var data = await new HttpClient().GetStringAsync(url);

            // Asenkron metod Delay
            await Task.Delay(4000);

            // Senkron metod Sleep
            //Thread.Sleep(5000);

            //5sn
            //5sn
            //5sn
            // Asenkronda toplam 5sn, Senkron olsaydı toplam 15 sn bekleyecekti.

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

        public async static Task DelayTask()
        {
            Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");

            List<string> urlList = new()
            {
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com"
            };

            List<Task<Content>> tasks = new();

            urlList.ToList().ForEach(x =>
            {
                tasks.Add(GetContentAsync(x));
            });

            Console.WriteLine("Before WhenAll");

            var contentList = await Task.WhenAll(tasks.ToArray());

            contentList.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Site);
            });

            //Console.WriteLine($"{firstData.Result.Site} - {firstData.Result.Length}");
        }
    }
}
