

// PlatformProviderManager — переиспользуемый координатор который по ключу выдаёт нужную реализацию. IMessengerSender — контракт с которым работает
// клиентский код, не зная про детали платформы. Конкретные классы реализуют специфику каждой платформы и не знают друг про друга. В реальном
// коде это те же самые классы, просто с более сложной доменной логикой внутри — например вместо Console.WriteLine идёт обращение к API
// мессенджера и работа с базой данных.


// Абстрактный менеджер провайдеров
public abstract class PlatformProviderManager<TKey, TProvider>
    where TKey : notnull
{
    private readonly IReadOnlyDictionary<TKey, TProvider> _providers;

    protected PlatformProviderManager(IReadOnlyDictionary<TKey, TProvider> providers)
        => _providers = providers;

    public TProvider Get(TKey key)
        => _providers.TryGetValue(key, out var provider)
            ? provider
            : throw new InvalidOperationException($"No provider for '{key}'");
}

// Платформы
public enum MessengerPlatform { Telegram, Max, Vk }

// Абстрактный провайдер
public interface IMessengerSender
{
    Task SendAsync(long userId, string message);
}

// Конкретные провайдеры
public class TelegramSender : IMessengerSender
{
    public Task SendAsync(long userId, string message)
    {
        Console.WriteLine($"[Telegram] → {userId}: {message}");
        return Task.CompletedTask;
    }
}

public class MaxSender : IMessengerSender
{
    public Task SendAsync(long userId, string message)
    {
        Console.WriteLine($"[Max] → {userId}: {message}");
        return Task.CompletedTask;
    }
}

public class VkSender : IMessengerSender
{
    public Task SendAsync(long userId, string message)
    {
        Console.WriteLine($"[VK] → {userId}: {message}");
        return Task.CompletedTask;
    }
}

// Конкретный менеджер
public class MessengerSenderFactory
    : PlatformProviderManager<MessengerPlatform, IMessengerSender>
{
    public MessengerSenderFactory(
        IReadOnlyDictionary<MessengerPlatform, IMessengerSender> providers)
        : base(providers) { }
}

public static class ExampleUsagePlatformManager
{
    public static async Task Run()
    {
        // Использование
        var factory = new MessengerSenderFactory(
            new Dictionary<MessengerPlatform, IMessengerSender>
            {
                [MessengerPlatform.Telegram] = new TelegramSender(),
                [MessengerPlatform.Max] = new MaxSender(),
                [MessengerPlatform.Vk] = new VkSender(),
            });

        // Клиентский код не знает про конкретные реализации
        var platform = MessengerPlatform.Max;
        var sender = factory.Get(platform);
        await sender.SendAsync(123456, "Ваши бонусы скоро сгорят!");

        // Добавление новой платформы — только новый класс + строка в словаре
        // Существующий код не меняется
    }
}