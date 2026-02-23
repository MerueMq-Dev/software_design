namespace Project.Infrastructure
{

    public class BankAccount : IBankAccount
    {
        private double _balance;

        public BankAccount(double initialBalance)
        {
            _balance = initialBalance;
        }

        public void Deposit(double sum) => _balance += sum;

        public void Withdraw(double sum) => _balance -= sum;

        public double GetBalance() => _balance;
    }
}
