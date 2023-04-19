using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    public class TaskResult
    {
        // İçerisinde async metod kullanıyoruz.await keywordüne ihtiyacımız var fakat metod senkron bir metod olduğu için Result keywordünü kullanıyoruz.
        public static string GetData()
        {
            var task = new HttpClient().GetStringAsync("https://www.google.com");

            // Asenkron olsaydı await ile dönecektik.

            // Önemli Not: Result'tan sonuç gelene kadar ana thread bloklanır.
            return task.Result.ToString();
        }
    }
}
