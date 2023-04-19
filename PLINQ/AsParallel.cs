using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    // PLINQ -> Paralel bir şekilde LINQ sorgularının çalıştırılmasına imkan veren bir teknolojidir.Okunabilirliği yüksek kodlara imkan tanır.
    public class AsParallel
    {
        public static void AsParallelSample()
        {
            var array = Enumerable.Range(1, 100).ToList();

            // Mod'u sıfır olanları getiriyoruz.(2'ye bölünebilen sayılar)
            var newArray = array.AsParallel().Where(x => x % 2 == 0);

            // 100 sayı 5 tane thread e bölündü.
            // 1.thread 1-20 arası.. 2-4-6 ...
            // 2.thread 21-40.. 22-24-26...
            // ..böyle devam ediyor.

            // IENumerable, veritabanında bir işlem yapılıyorsa ve where sorgusu varsa IENumerable ile beraber dbye gider ve tüm datayı çeker ve sonra where koşulunu uygular.
            // IQuerable, veritabanına, yazılan sorgu ne ise onu getirir.(where koşuluyla beraber getirir datayı)

            newArray.ToList().ForEach(x =>
            {
                Console.WriteLine(x);
            });
        }
    }
}
