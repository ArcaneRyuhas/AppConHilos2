namespace AppConHilos2;

using System;
using System.Threading;


class Program
{
    static void Main()
    {
        Thread currentThread = Thread.CurrentThread;
        currentThread.Name = "Hilo principal";
        currentThread.Priority = ThreadPriority.Lowest;
        currentThread.IsBackground = false;

        Console.WriteLine("Thread id: {0}", currentThread.ManagedThreadId);
        Console.WriteLine("Thread name: {0}", currentThread.Name);
        Console.WriteLine("Thread state: {0}", currentThread.ThreadState);
        Console.WriteLine("Thread is background: {0}", currentThread.IsBackground);
        Console.WriteLine("Thread priority: {0}", currentThread.Priority);
        Console.WriteLine("Thread culture: {0}", currentThread.CurrentCulture.Name);
        Console.WriteLine("Thread UI culture: {0}", currentThread.CurrentUICulture.Name);
        Console.WriteLine();

        Thread workerThread = new Thread(new ParameterizedThreadStart(Print));
        workerThread.Name = "Hilo de print";
        CancellationTokenSource cts = new CancellationTokenSource();
        workerThread.Start(cts.Token);

        for(int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Principal thread: {i}");
            Thread.Sleep(200);
        }

        if (workerThread.IsAlive){
            cts.Cancel();
        }

    }

    static void Print(Object? obj)
    {
        if (obj == null)
        {
            return;
        }

        CancellationToken cancellationToken = (CancellationToken)obj;
        
        Thread currentThread = Thread.CurrentThread;
        currentThread.Priority = ThreadPriority.Highest;
        currentThread.IsBackground = false;

        Console.WriteLine("Thread id: {0}", currentThread.ManagedThreadId);
        Console.WriteLine("Thread name: {0}", currentThread.Name);
        Console.WriteLine("Thread state: {0}", currentThread.ThreadState);
        Console.WriteLine("Thread is background: {0}", currentThread.IsBackground);
        Console.WriteLine("Thread priority: {0}", currentThread.Priority);
        Console.WriteLine("Thread culture: {0}", currentThread.CurrentCulture.Name);
        Console.WriteLine("Thread UI culture: {0}", currentThread.CurrentUICulture.Name);
        Console.WriteLine();

        for (int i = 11; i < 20; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("En la iteración {0}, la cancelación ha sido solicitada...", i);
                break;
            }
            Console.WriteLine($"Print thread: {i}");
            Thread.Sleep(1000);
        }
    }
}
