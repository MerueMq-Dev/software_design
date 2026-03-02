namespace Project.Infrastructure;

// Во втором примере возникает взаимная блокировка, потому что потоки захватывают ресурсы в разном порядке.
// Один берёт сначала lock1, потом lock2, а второй — наоборот. В какой-то момент каждый из них может ждать
// ресурс, который уже удерживает другой, и программа зависает. Вывод простой: при работе с несколькими
// блокировками нужно всегда придерживаться одного и того же порядка их захвата — тогда deadlock не возникнет.

class DeadlockSolution
{
    private static readonly object lock1 = new object();
    private static readonly object lock2 = new object();

    static void Main()
    {
        Thread thread1 = new Thread(DoWork);
        Thread thread2 = new Thread(DoWork);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Finished");
    }

    static void DoWork()
    {
        // Всегда сначала lock1
        lock (lock1)
        {
            Thread.Sleep(50);

            // Потом lock2
            lock (lock2)
            {
                Console.WriteLine("Work done");
            }
        }
    }
}
