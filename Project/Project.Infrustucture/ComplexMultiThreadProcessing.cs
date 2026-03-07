public class ComplexMultiThreadProcessing
{
    private static readonly int SIZE = 1_000_000;
    private static readonly int THREADS = 4;
    private static int sum = 0;

    public static void Run()
    {
        var rand = new Random();
        var data = Enumerable.Range(0, SIZE)
            .Select(_ => rand.Next(100))
            .ToArray();

        int chunkSize = SIZE / THREADS;

        var threads = Enumerable.Range(0, THREADS)
            .Select(i => {
                int start = i * chunkSize;
                int end = (i == THREADS - 1) ? SIZE : start + chunkSize;
                int[] chunk = data[start..end];
                return new Thread(() =>
                {
                    int localSum = chunk.Sum();
                    Interlocked.Add(ref sum, localSum);
                });
            })
            .ToArray();
        try
        {
            Array.ForEach(threads, t => t.Start());
            Array.ForEach(threads, t => t.Join());
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("Sum of all elements: " + sum);
    }
}