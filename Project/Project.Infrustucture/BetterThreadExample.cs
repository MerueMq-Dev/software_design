namespace Project.Infrastructure;

public class BetterThreadExample
{
    private static int counter = 0;

    public static void Run()
    {
        Action task = () =>
        {
            for (int i = 0; i < 1000; i++)
            {
                Interlocked.Increment(ref counter);
            }
        };


        Thread thread1 = new Thread(new ThreadStart(task));
        Thread thread2 = new Thread(new ThreadStart(task));

        thread1.Start();
        thread2.Start();
      
        thread1.Join();
        thread2.Join();
        
        Console.WriteLine(counter);
    }
}
