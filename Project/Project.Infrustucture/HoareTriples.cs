namespace Project.Infrastructure;

public static class HoareTriples
{
    // {P: number — любое целое} Abs(number)
    // {Q: result >= 0 && result == |number|}
    private static int Abs(int number) =>
        number >= 0 ? number : -number;

    // {P: firstNumber, secondNumber — любые целые} Max(firstNumber, secondNumber)
    // {Q: result >= firstNumber && result >= secondNumber
    //  && (result == firstNumber || result == secondNumber)}
    private static int Max(int firstNumber, int secondNumber) =>
        firstNumber < secondNumber ? secondNumber : firstNumber;

    // {P: firstNumber, secondNumber — любые целые} MaxAbs(firstNumber, secondNumber)
    // {Q: result == Max(Abs(firstNumber), Abs(secondNumber)) && result >= 0}
    public static int MaxAbs(int firstNumber, int secondNumber) =>
        Max(Abs(firstNumber), Abs(secondNumber));
}