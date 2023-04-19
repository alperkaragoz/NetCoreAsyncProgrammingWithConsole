using System.Diagnostics;
using TaskConsoleApp;
using static TaskConsoleApp.WhenAll;
using static TaskConsoleApp.WhenAny;
using static TaskConsoleApp.WaitAll;
using static TaskConsoleApp.Delay;
using static TaskConsoleApp.TaskStartNew;
using static TaskConsoleApp.FromResult;
using static TaskConsoleApp.TaskResult;
using static TaskConsoleApp.TaskProperties;

internal class Program
{
    private async static Task Main(string[] args)
    {
        //await WhenAnyTask();
        //await WhenAllTask();
        //await WaitAllTask();
        //await DelayTask();
        //await TaskStartNewTask();
        //await FromResultTask();

        //At TaskResult.cs
        //Console.WriteLine(GetData());

        // At TaskProperties.cs
        await TaskProps();
    }
}