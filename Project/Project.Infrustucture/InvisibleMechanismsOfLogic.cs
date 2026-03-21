using System.Text.Json;

namespace Project.Infrastructure;

// В коде есть несколько проблем:
// Первая проблема это проблема с зависимостями, библиотека jackson-databind указана 2 раза с разными версиями,
// что может привести к непредсказуемому результату.
//
// Вторая проблема в том, что переменная result объявлена внутри первого блока try, а мы пытаемся использовать её уже 
// во втором блоке try, в котором она будет недоступна — потому что фигурные скобки { } первого блока ограничивают её видимость.
//
// Третья проблема в том, что мы передаём HashMap.class в метод readValue, и Jackson знает только что нужно создать HashMap, 
// но не знает какие типы у ключей и значений — потому что Java стирает информацию о дженериках в рантайме, оставляя только 
// сырой тип. Из-за этого Jackson сам решает каким типом десериализовать числа, и например 30 может стать Integer в одной версии
// и Long в другой, что в итоге приведёт к ClassCastException в рантайме. В C# стирания типов нет, но проблема неопределённости
// типа всё равно проявляется — если использовать JsonSerializer.Deserialize<Dictionary<string, object>>, то object 
// слишком широкий тип, и 30 десериализуется не как int, а как JsonElement, и попытка 
// кастануть его напрямую к int приведёт к InvalidCastException в рантайме.
//
// Четвертая проблема в том, что мы ловим IOException вместо конкретных JsonParseException и JsonMappingException — они оба наследуются
// от IOException поэтому код скомпилируется, но мы теряем возможность по-разному реагировать на ошибку парсинга и ошибку маппинга.

public class InvisibleMechanismsOfLogic
{
    public static void RunExample()
    {
        string jsonString = "{\"name\":\"John\", \"age\":30}";

        Dictionary<string, object> result = null;

        try
        {
            // Парсим JSON в словарь
            result = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

            Console.WriteLine("Name: " + result["name"]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // аналог pretty printer
            };

            string prettyJson = JsonSerializer.Serialize(result, options);
            Console.WriteLine("Pretty JSON: " + prettyJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}


