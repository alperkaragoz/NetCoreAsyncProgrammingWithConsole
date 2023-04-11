using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetCoreAsyncProgramming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        public async Task<IActionResult> GetContentAsync()
        {

            // ------- METHOD 1- ContinueWith ile ------->

            // GetStringAsync metodundan sonra ContinueWith içerisindeki kod bloğu çalışacaktır.
            // GetStringAsync metodundan dönen data Task<string> tipinde olduğundan dolayı ContinueWith içerisindeki "data" 'da Task<string> tipinde olmaktadır.
            var task = new HttpClient().GetStringAsync("https://www.google.com")
                .ContinueWith((data) =>
                {
                    Debug.Write($"Google Length: {data.Result.Length}");
                });

            // Eğer ContinueWith içerisinde çok fazla process/kod var ise alternatif olarak geriye Task<string> tipinde değer dönen bir metod yazarak daha clean hale getirebiliriz. Örn: Run metodu.
            // Örnek kullanımı
            var task2 = new HttpClient().GetStringAsync("https://www.google.com")
                .ContinueWith(Run);

            // Önce alt satır çalışacak.Sonra task içerisindeki işlemler çalışacak.Çünkü await keywordünü en alttaki satırda kullanıyoruz.
            Debug.Write("Other Processes");

            await task;

            //// await kullandığımızda datayı istiyoruz.
            //var data = await task;


            // ------ METHOD 1- ContinueWith olmadan ------>


            // Bu durumda önce 
            var taskWithoutContinueWith = new HttpClient().GetStringAsync("https://www.google.com");

            Debug.Write("Other Processes Without ContinueWith");

            var dataWithoutContinueWith = await taskWithoutContinueWith;
            Debug.Write($"Google Length: {dataWithoutContinueWith.Length}");


            return Ok(task);
        }

        public static void Run(Task<string> data)
        {
            Debug.Write($"Google Length: {data.Result.Length}");
        }
    }
}
