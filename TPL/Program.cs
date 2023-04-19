using static TPL.ForeachParallel;
using static TPL.ParallelThreadLocalVariables;

public class Program
{
    //  Task Parallel Library -> Kodları 1'den fazla threadte çalıştırabiliriz.(MultiThread).Built-in kütüphanedir.
    //    Örneğin: 1000 tane itema sahip array var ve bu her bir item ı bir metoda gönderiyoruz.Bu metodtan bize sonuç geliyor.Bu metodun çok yoğun algoritmiksel    ve matematiksel işlemler yaptığını varsayarsak TPL kullanmamız uygun olacaktır.Bunu tek bir threadte halletmeye çalışsak çok uzun sürecektir.TPL'in        birkaç metodu var, bu metodlar sayesinde hızlı bir şekilde gerçekleştirebiliriz.

    // Race Condition -> Çok threadli uygulamalarda 1'den çok threadin paylaşılan bir hafıza alanına aynı anda erişmeye çalışmasıyla meydana geliyor.
    //    Örneğin; integer 17 olan bir değer var.Bu değeri foreache sokuyoruz.2 tane threadimiz olduğunu varsayarsak, 1.thread 17 değerini alıyor ve 1               arttırıyor.2.thread'te 17 değerini alıyor ve 1 arttırıyor.MultiThread uygulamalarda olması yüksek ihtimal bir durumdur.Bu durumu çözmek için, ana          threadi kilitliyoruz.1.thread 17'yi alıp güncelleyene kadar başka bir threadin bu değeri almasını engelliyoruz.
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //ForeachParallelTask();
        //ForeachParallelTaskSecondExam();
        ParallelThread();
    }
}