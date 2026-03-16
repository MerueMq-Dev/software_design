namespace Project.Infrastructure;

public static class Array
{

    // { array != null && array.Count > 0 } FindMax(array) { result = max(array) }
   public static int FindMax(this IList<int> array)
   {
        if (array == null)
            throw new ArgumentNullException();


        if (array.Count == 0)
            throw new ArgumentException();
        

        var current = array[0];
        for (int i = 1; i < array.Count; i++) 
        {
            if (current < array[i])
                current = array[i];
        }
        return current;
   }


    // Доказательство корректности FindMax.
    // Тройка Хоара:
    // {P: array != null && array.Count > 0} FindMax(array) { Q: result == Max(array) && array.Contains(result) }.
    // 
    //  Инвариант цикла:
    // 1 <= i <= array.Count и current = Max(array[0..i - 1]).

    //  Доказательство инварианта.

    // Инициализация.
    // До начала первой итерации цикла: i = 1 и current = array[0].Проверка инварианта: 1 <= 1 <= array.Count
    // и current = Max(array[0..0]) = array[0].Инвариант истинен в начале.

    // Сохранение инварианта.
    // Предположим (доказательство по индукции), что инвариант истинен перед некоторой итерацией цикла,
    // где 1 <= i < array.Count и current = Max(array[0..i - 1]). Во время выполнения цикла выполняется условие if (current<array[i])
    // current = array[i]. Рассмотрим два случая. Если current >= array[i], то current не изменяется,
    // следовательно current = Max(array[0..i - 1]) = Max(array[0..i]). Если current<array[i], то current = array[i],
    // следовательно current = Max(array[0..i]). В обоих случаях после выполнения тела цикла current = Max(array[0..i]).
    // После увеличения счётчика цикла i = i + 1 получаем current = Max(array[0..i - 1]).
    // Таким образом инвариант по-прежнему истинен: 1 <= i <= array.Count и current = Max(array[0..i - 1]).

    // Завершение.
    // Цикл завершается, когда i становится равным array.Count. На этом этапе current = Max(array[0..array.Count - 1]),
    // так как по инварианту после последней итерации i = array.Count и current = Max(array[0..array.Count - 1]).
    // Следовательно current = Max(array).Функция возвращает значение current, то есть result = current.
    // Следовательно выполняется постусловие: result ∈ array && ∀ x ∈ array: result >= x. Корректность метода доказана.

}
