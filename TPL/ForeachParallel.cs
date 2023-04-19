using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL // Task Parallel Library
{
    public class ForeachParallel
    {
        public static void ForeachParallelTask()
        {
            Stopwatch sw = Stopwatch.StartNew();

            string picturePath = @"D:\pictures";

            var files = Directory.GetFiles(picturePath);


            // İlgili pathteki resimleri küçük resim haline çeviriyoruz.
            // Asenkron değil multithread çalışan kod bloğu;
            Parallel.ForEach(files, file =>
            {
                Console.WriteLine($"Thread No: {Thread.CurrentThread.ManagedThreadId}");
                Image image = new Bitmap(file);
                var thumbnail = image.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                thumbnail.Save(Path.Combine(picturePath, "thumbnail", Path.GetFileName(file)));
            });

            sw.Stop();

            Console.WriteLine($"Process Finished!{sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();

            files.ToList().ForEach(file =>
            {
                Console.WriteLine($"Thread No: {Thread.CurrentThread.ManagedThreadId}");
                Image image = new Bitmap(file);
                var thumbnail = image.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                thumbnail.Save(Path.Combine(picturePath, "thumbnail", Path.GetFileName(file)));
            });
            sw.Stop();
            Console.WriteLine($"Process Finished!{sw.ElapsedMilliseconds}");
        }

        public static void ForeachParallelTaskSecondExam()
        {
            long fileBytes = 0;

            Stopwatch sw = Stopwatch.StartNew();

            string picturePath = @"D:\pictures";

            var files = Directory.GetFiles(picturePath);

            // Asenkron değil multithread çalışan kod bloğu; 1'den fazla thread çalıştırır.
            Parallel.ForEach(files, file =>
            {
                Console.WriteLine($"Thread No: {Thread.CurrentThread.ManagedThreadId}");

                FileInfo f = new FileInfo(file);

                // ref keywordü value typeların reference type olarak davranmasını sağlar.
                // Interlocked Add metodu herhangi bir thread bu koda geldiğinde bu değeri eklediğinde, bu değer eklenene kadar başka bir threadin fileBytes'a erişmesini engelliyor.Bu sayede her zaman başka threadler en son bitmiş olan datayı(fileBytes içerisindeki güncel datayı) alıyor.
                Interlocked.Add(ref fileBytes, f.Length);

                Console.WriteLine($"Total Size: {fileBytes.ToString()}");


            });

            sw.Stop();

            Console.WriteLine($"Process Finished!{sw.ElapsedMilliseconds}");
            sw.Reset();
        }


    }
}
