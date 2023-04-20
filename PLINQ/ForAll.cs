using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    public class ForAll
    {
        public static void ForAllSample()
        {
            var array = Enumerable.Range(1, 100).ToList();

            // Mod'u sıfır olanları getiriyoruz.(2'ye bölünebilen sayılar)
            var newArray = array.AsParallel().Where(x => x % 2 == 0);

            // Eğer PLINQ kullanıyorsak ve gelen datadaki sonuçları görmek istiyorsak normal foreach kullanmak performans anlamında iyi değildir.Bunun yerine ForAll metodunu kullanıyoruz.
            newArray.ForAll(x =>
            {
                Thread.Sleep(500);
                Console.WriteLine(x);
            });
        }
    }
}
