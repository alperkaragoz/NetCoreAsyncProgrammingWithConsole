// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PlinqDbSamples.Models;



internal class Program
{
    // // EXCEPTION satırlarında -> Bunu yaparak Exception içerisinde kod kırılmayıp devam edecek.(Arka arkaya 1'den fazla hata alınacaksa). Eğer hata alındığında kodun kesilmesini istiyorsak lamda ile yazıyoruz.Satır 78 -> var query = products.AsParallel().Where(p => isControl(p));
    private static bool isControl(Product p)
    {
        try
        {
            return p.Name[2] == 'a';
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Index out of bound bla bla bla ...");
            return false;
        }
    }
    private static void WriteLog(Product p)
    {
        Console.WriteLine($"{p.Name} saved to log");
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        AdventureWorks2019Context context = new AdventureWorks2019Context();

        // AsParallel()' den sonra paralel kullanılıyor.where'den itibaren AsParallel kullanılıyor.
        // Elimizde var olan array üzerinde bir sorgu yapmak istiyorsak parallel bu durumda işe yarar.
        //var product = (from p in context.Products.AsParallel()
        //               where p.ListPrice > 10M
        //               select p).Take(10);

        // WithDegreeOfParallelism -> Sorguların paralel olarak kaç tane işlemcide çalışacağını belirtmemizi sağlayan metodtur.
        //context.Products.AsParallel().WithDegreeOfParallelism(2).ForAll(p =>
        //{
        //    WriteLog(p);
        //});


        // WithExecutionMode -> AsParallel dedikten sonra yüzde 100 paralel çalışacak diye bir kural yoktur.Fakat kesinlikle paralel çalışmasını istiyorsak WithExecutionMode metodunu kullanıyoruz.Ama uygun olan PLINQ'e bırakmaktır.Genellikle WithExecutionMode'u test ederken kullanıyoruz.
        // ParallelExecutionMode.ForceParallelism seçilirse Paralel kullanmayı zorunlu hale getiririz.ParallelExecutionMode.Default seçersek paralel kullanımını LINQ' e bırakmış oluruz.
        //context.Products.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).ForAll(p =>
        //{
        //    WriteLog(p);
        //});



        // AsOrdered -> Var olan araylar içerisinde sıralama nasılsa, çıktı da o şekilde muhafaza edilir. AsOrdered, veritabanındaki sıralama ile aynı gelmektedir.AsOrdered olmadığı durumlarda her zaman aynı sonuç olmayabilir.AsOrdered performansı düşürebilir.
        //context.Products.AsParallel().AsOrdered().Where(p => p.ListPrice > 10M).ToList().ForEach(x =>
        //{
        //    Console.WriteLine($"{x.Name} - {x.ListPrice}");
        //});

        //product.ToList().ForEach(p =>
        //{
        //    Console.WriteLine(p);
        //});
        //// Yukardaki kod veritabanına gitmez.Ne zaman ki ForAll metodunu kullanırız, o zaman veritabanına gider ve sorguyu getirir.
        //product.ForAll(x =>
        //{
        //    Console.WriteLine(x.Name);
        //});


        // EXCEPTION
        var products = context.Products.Take(100).ToArray();

        // Hata fırlatmak için;
        products[3].Name = "##";
        products[5].Name = "##";
        var query = products.AsParallel().Where(p => isControl(p));

        try
        {
            query.ForAll(p =>
            {
                Console.WriteLine($"{p.Name}");
            });
        }
        // AggregateException içerisinde 1'den fazla hatayı tutabilir.
        catch (AggregateException aex)
        {
            aex.InnerExceptions.ToList().ForEach(ex =>
            {
                if (ex is IndexOutOfRangeException)
                    Console.WriteLine($"Error: Index out of array bla bla bla..");
                //Console.WriteLine($"Error: {ex.GetType().Name}"); // IndexOutOfRangeException döndürür.
            });
        }

        Console.ReadLine();
    }
}