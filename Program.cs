using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ProducentConsumerProblem;



class Program
{
    static void Main(string[] args)
    {
        int queueCapacity = 10;
        BlockingCollection<object> queue = new BlockingCollection<object>(queueCapacity);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        Task producerTask = Task.Run(() => new ProducentX(queue, cancellationTokenSource.Token).StartProducing());
        Task consumerTask = Task.Run(() => new ConsumerX(queue, cancellationTokenSource.Token).StartConsuming());

        Console.WriteLine("Stiskni Enter pro ukončení programu...");
        Console.ReadLine();

        cancellationTokenSource.Cancel();
        Task.WaitAll(producerTask, consumerTask);

        Console.WriteLine("Program byl ukončen.");
    }
}



