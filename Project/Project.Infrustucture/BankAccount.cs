namespace Project.Infrastructure;

public class BankAccount : IBankAccount
{
    private double _balance;

    public BankAccount(double initialBalance)
    {
        if (initialBalance <= 0)
            throw new ArgumentOutOfRangeException("Баланс не может быть отрицательным или равным нулю");

        _balance = initialBalance;
    }

    public void Deposit(double sum)
    {
        if (sum <= 0)
            throw new ArgumentOutOfRangeException("Сумма депозита не может быть отрицательной или равной нулю");

        _balance += sum;
    }
    public void Withdraw(double sum)
    {       
        if (sum <= 0)
            throw new ArgumentOutOfRangeException("Сумма для снятия не может быть отрицательной или равной нулю");

        if (sum > _balance)
            throw new InvalidOperationException("На балансе не достаточно средств");

        _balance -= sum;
    }
    public double GetBalance() => _balance;
}
