using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    public class TaskProperties
    {
        public async static Task TaskProps()
        {
            //  Debug modda Watch ile task propertieslerini incelediğimizde;
            //  IsCompletedSuccessfully property'si; hata alınmadan başarılı bir şekilde bittiğini gösterir.(true durumunda).
            //  IsCompleted property'si; başarılı veya başarısız bir şekilde bittiğini gösterir.(true durumunda)
            //  IsFaulted property'si; eğer task hata fırlatsaydı true olacaktı.
            //  Status property'si; RunToCompletion olması durumunda çalışmanın tamamlandığını gösterir. 
            //  Exception property'si; Hataların yazıldığı propertydir.
            Task task = Task.Run(() =>
            {
                Console.WriteLine("task running");
            });
            await task;

            Console.WriteLine("task finished");
        }
    }
}
