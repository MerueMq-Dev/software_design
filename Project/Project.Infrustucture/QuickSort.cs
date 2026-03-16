namespace Project.Infrastructure;
public static class QuickSort
{   
    public static void Sort(this int[] array)
    {
        if (array == null || array.Length <= 1)
            return;

        Sort(array, 0, array.Length - 1);
    }
    
    private static void Sort(int[] array, int left, int right)
    {
        if (left >= right)
            return;

        int pivotIndex = Partition(array, left, right);

        Sort(array, left, pivotIndex - 1);
        Sort(array, pivotIndex + 1, right);
    }

    private static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, right);
        return i + 1;
    }

    private static void Swap(IList<int> array, int firstNumber, int secondNumber)
       => (array[firstNumber], array[secondNumber]) = (array[secondNumber], array[firstNumber]);
}


// Доказательство корректности Sort.
// Тройка Хоара: {array != null && array.Count > 0} Sort(array) {array отсортирован по неубыванию}.
// Предусловие P: массив существует и массив не пуст array != null && array.Count > 0.
// Постусловие Q: после выполнения метода для любых индексов i < j выполняется array[i] ≤ array[j], то есть массив отсортирован.

// Алгоритм быстрой сортировки использует метод Partition, который выбирает опорный элемент pivot и размещает его на правильной позиции,
// разделяя массив на элементы меньше или равные pivot и элементы больше pivot.

// Инвариант цикла в методе Partition: array[left..i] ≤ pivot и array[i+1..j-1] > pivot. Это означает,
// что все элементы слева от индекса i меньше или равны pivot, а элементы между i+1 и j-1 больше pivot.

// Инициализация:
// перед первой итерацией i = left - 1 и j = left.
// Диапазоны array[left..i] и array[i+1..j-1] пустые, поэтому инвариант выполняется.

// Процесс:
// предположим, что инвариант выполняется перед некоторой итерацией.
// Рассматривается элемент array[j]. Если array[j] ≤ pivot, выполняется i++ и Swap(array, i, j),
// и элемент переносится в область элементов ≤ pivot. Если array[j] > pivot, элемент остаётся в области элементов > pivot.
// В обоих случаях инвариант сохраняется.

// Завершение:
// цикл заканчивается, когда j достигает right. В этот момент элементы array[left..i] ≤ pivot, а элементы array[i+1..right-1] > pivot.
// После выполнения Swap(array, i+1, right) опорный элемент перемещается на позицию i+1. Теперь слева от него находятся элементы ≤ pivot,
// а справа элементы > pivot, то есть pivot стоит на своей правильной позиции.
// После этого метод Sort рекурсивно сортирует левую и правую части массива. Поскольку pivot уже находится на правильной позиции,
// а обе части сортируются тем же алгоритмом, после завершения рекурсии весь массив становится отсортированным.
// Следовательно выполняется постусловие: массив отсортирован по неубыванию, корректность алгоритма доказана.
