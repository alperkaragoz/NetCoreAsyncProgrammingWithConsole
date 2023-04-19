using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL // Task Parallel Library
{
    public static class ParallelThreadLocalVariables
    {
        public static void ParallelThread()
        {
            int total = 0;

            //Parallel.ForEach(Enumerable.Range(1, 100).ToList(), (x) =>
            //{
            //    // Çok performanslı bir yöntem değil.
            //    Interlocked.Add(ref total, x);
            //});

            // Bizim istediğimiz her bir thread kendi payına düşen kendi içerisinde toplasın ve en son eklesin.
            // ()=>0 -> Sıfır ile başlasın.
            Parallel.ForEach(Enumerable.Range(1, 100).ToList(), () => 0, (x, loop, subtotal) =>
            {
                subtotal += x;
                return subtotal;
                // Aşağıdaki virgül sonrası değer localFinally o threadin kendi içerisinde hesaplamış olduğu değer.Action'lar herhangi bir değer dönmeyen metodu işaret eder, Function'lar mutlaka değer dönen bir metodu işaret eder.
            }, (y) => Interlocked.Add(ref total, y));

            Console.WriteLine(total);
        }
    }
}
