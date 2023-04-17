using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    public class WhenAll
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

        public async static Task WhenAllTask()
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

            // Eğer await kullanmamış olsaydık Task<Content> alacaktı contents objesi.await keywordü ile beraber Content[] alıyoruz.
            var contents = await Task.WhenAll(tasks.ToArray());

            contents.ToList().ForEach(content =>
            {
                Console.WriteLine($"{content.Site} Length:{content.Length}");
            });
        }
    }
}
