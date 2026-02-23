namespace Project.Infrastructure
{
    public interface IBankAccount
    {
        void Deposit(double sum);
        double GetBalance();
        void Withdraw(double sum);
    }
}