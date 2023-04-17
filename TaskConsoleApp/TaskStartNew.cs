using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    // Run metodu gibi içerisinde yazdığımız kodları ayrı bir threadte çalıştırır.Farkı; StartNew metoduna bir obje geçebiliyoruz.Task işlemi bittiğinde bu geçtiğimiz objeyi alabiliyoruz.
    public class TaskStartNew
    {
        public async static Task TaskStartNewTask()
        {
            var task = Task.Factory.StartNew((obj) =>
            {
                Console.WriteLine("task processed");
                var status = obj as Status;
                status.ThreadId = Thread.CurrentThread.ManagedThreadId;
            }, new Status() { Date = DateTime.Now });

            await task;

            Status s=task.AsyncState as Status;

            Console.WriteLine(s.Date);
            Console.WriteLine(s.ThreadId);
        }
    }

    public class Status
    {
        public int ThreadId { get; set; }
        public DateTime Date { get; set; }
    }
    public class Content
    {
        public string? Site { get; set; }
        public int Length { get; set; }
    }
}
