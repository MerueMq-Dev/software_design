namespace Project.Infrastructure;

class Button
{
    public event Action OnClick;
    public void Click() => OnClick?.Invoke();
}

class TickEventArgs : EventArgs
{
    public int ElapsedSeconds { get; init; }
}

class SimpleTimer
{
    public event EventHandler<TickEventArgs> Tick;

    public void Start(int intervalMs, int ticks)
    {
        for (int i = 1; i <= ticks; i++)
        {
            Thread.Sleep(intervalMs);
            Tick?.Invoke(this, new TickEventArgs { ElapsedSeconds = i });
        }
    }
}

static class SynchronizationExamples
{
    // ManualResetEventSlim — хранит состояние: открыт или закрыт.
    // Интересно что Set() разблокирует сразу все потоки на Wait(),
    // а не по одному как в AutoResetEvent. И остаётся открытым пока не вызвать Reset().
    public static void ManualResetEvent()
    {
        Console.WriteLine("ManualResetEventSlim");

        var gate = new ManualResetEventSlim(false);
        var threads = new Thread[3];

        for (int i = 0; i < 3; i++)
        {
            int id = i;
            threads[i] = new Thread(() =>
            {
                Console.WriteLine($"Поток {id} ожидает сигнала...");
                gate.Wait();
                Console.WriteLine($"Поток {id} получил сигнал, продолжает!");
            });
            threads[i].Start();
        }

        Thread.Sleep(1000);
        Console.WriteLine();
        Console.WriteLine("Отправляю сигнал!");
        Console.WriteLine();
        gate.Set();

        foreach (var t in threads) t.Join();
    }

    // AutoResetEvent — похож на ManualResetEvent, но сбрасывается сам после каждого WaitOne().
    // То есть Set() пропускает ровно один поток, остальные продолжают ждать.
    // Удобно когда нужно строго по одному — как в Producer/Consumer.
    public static void AutoResetEvent()
    {
        Console.WriteLine("AutoResetEvent");
        Console.WriteLine();

        var signal = new AutoResetEvent(false);

        var producer = new Thread(() =>
        {
            for (int i = 1; i <= 3; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Producer отправляет сигнал #{i}");
                signal.Set();
            }
        });

        var consumer = new Thread(() =>
        {
            for (int i = 1; i <= 3; i++)
            {
                signal.WaitOne();
                Console.WriteLine($"Consumer получил сигнал #{i}");
            }
        });

        producer.Start();
        consumer.Start();
        producer.Join();
        consumer.Join();
    }

    // CountdownEvent — нужен когда хочется дождаться завершения нескольких потоков.
    // Можно было использовать Join() на каждом потоке, но тогда пришлось бы ждать их по очереди.
    // Здесь каждый поток вызывает Signal(), счётчик уменьшается, и Wait() отпускает когда дойдёт до нуля.
    public static void CountdownEventDemo()
    {
        Console.WriteLine("CountdownEvent");
        Console.WriteLine();

        var countdown = new CountdownEvent(3);
        var threads = new Thread[3];

        for (int i = 0; i < 3; i++)
        {
            int id = i;
            threads[i] = new Thread(() =>
            {
                Thread.Sleep(new Random().Next(300, 1000));
                Console.WriteLine($"Поток {id} завершил работу");
                countdown.Signal();
            });
            threads[i].Start();
        }

        countdown.Wait();
        Console.WriteLine();
        Console.WriteLine("Все потоки завершились!");

        foreach (var t in threads) t.Join();
    }

    // event Action — самый простой вариант событий, когда данные передавать не нужно.
    // Издатель просто вызывает OnClick?.Invoke() и понятия не имеет кто подписан.
    // Подписчики добавляются через += и вызываются все по очереди.
    public static void EventAction()
    {
        Console.WriteLine("event Action");

        var button = new Button();

        button.OnClick += () => Console.WriteLine("Logger Клик зафиксирован");
        button.OnClick += () => Console.WriteLine("Analytics Отправляем статистику");
        button.OnClick += () => Console.WriteLine("UI Анимируем кнопку");

        Console.WriteLine("Нажата кнопка!");
        Console.WriteLine();
        button.Click();
    }

    // EventHandler<T> — то же самое что event Action, но с возможностью передать данные.
    // sender это кто отправил событие, e это данные — наследник EventArgs.
    // Судя по всему именно этот вариант считается стандартным в .NET, его везде и используют.
    public static void EventHandler()
    {
        Console.WriteLine("EventHandler<T>");
        Console.WriteLine();

        var timer = new SimpleTimer();

        timer.Tick += (s, e) => Console.WriteLine($"Logger Прошло секунд: {e.ElapsedSeconds}");
        timer.Tick += (s, e) => Console.WriteLine($"Monitor Uptime: {e.ElapsedSeconds}s\n");

        timer.Start(intervalMs: 1000, ticks: 3);
    }
}
