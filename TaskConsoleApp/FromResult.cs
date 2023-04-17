using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    // Object parametresi alıyor.Bu object'i geri Task nesnesi dönüyor.Metotdan geriye alınan static bir data dönülmek isteniyorsa FromResult kullanılabilir.Genellikle Cachelenmiş datayı dönmek amacıyla kullanılır.
    public class FromResult
    {
        public static string CacheData { get; set; }
        public async static Task FromResultTask()
        {
            CacheData = await GetDataAsync();
            Console.WriteLine(CacheData);
        }

        public static Task<string> GetDataAsync()
        {
            // 
            if (string.IsNullOrEmpty(CacheData))
                return File.ReadAllTextAsync("file.txt");
            else
                return Task.FromResult<string>(CacheData);
        }
    }
}
