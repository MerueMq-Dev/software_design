using FluentAssertions;
using Project.Infrastructure;

namespace Project.Tests
{
    public class BankAccountTests
    {
        // Withdraw с положительной суммой — баланс уменьшается
        [Theory]
        [InlineData(100, 30, 70)]
        [InlineData(50, 50, 0)]
        [InlineData(30, 50, -20)]
        public void Withdraw_PositiveAmount_BalanceDecreases(double balanceInitSum, double sum, double expectedSum)
        {
            IBankAccount bankAccount = new BankAccount(balanceInitSum);
            bankAccount.Withdraw(sum);
            var newBalance = bankAccount.GetBalance();
            newBalance.Should().Be(expectedSum);
        }

        // Withdraw с отрицательной суммой — баланс увеличивается
        [Theory]
        [InlineData(100, -30, 130)]
        [InlineData(0, -10, 10)]
        [InlineData(50, -50, 100)]
        public void Withdraw_NegativeAmount_BalanceIncreases(double balanceInitSum, double sum, double expectedSum)
        {
            IBankAccount bankAccount = new BankAccount(balanceInitSum);
            bankAccount.Withdraw(sum);
            var newBalance = bankAccount.GetBalance();
            newBalance.Should().Be(expectedSum);
        }

        // Withdraw всего баланса — результат ровно 0
        [Theory]
        [InlineData(100, 100, 0)]
        public void Withdraw_ExactBalance_ResultsInZeroBalance(double balanceInitSum, double sum, double expectedSum) { }

        // Withdraw больше баланса — уходим в минус

        [Theory]
        [InlineData(100, 150, -50)]
        public void Withdraw_MoreThanBalance_BalanceBecomesNegative(double balanceInitSum, double sum, double expectedSum) { }

        // Withdraw нуля — баланс не меняется
        [Theory]
        [InlineData(100, 0, 100)]
        public void Withdraw_ZeroAmount_BalanceUnchanged(double balanceInitSum, double sum, double expectedSum)
        {
            IBankAccount bankAccount = new BankAccount(balanceInitSum);
            bankAccount.Withdraw(sum);
            var newBalance = bankAccount.GetBalance();
            newBalance.Should().Be(expectedSum);
        }

        // Deposit с отрицательной суммой — баланс уменьшается
        [Theory]
        [InlineData(100, -30, 70)]
        [InlineData(50, -50, 0)]
        [InlineData(30, -50, -20)]
        public void Deposit_NegativeAmount_BalanceDecreases(double balanceInitSum, double sum, double expectedSum)
        {
            IBankAccount bankAccount = new BankAccount(balanceInitSum);
            bankAccount.Deposit(sum);
            var newBalance = bankAccount.GetBalance();
            newBalance.Should().Be(expectedSum);
        }

        // Deposit с положительной суммой — баланс увеличивается
        [Theory]
        [InlineData(100, 30, 130)]
        [InlineData(0, 50, 50)]
        [InlineData(50, 50, 100)]
        public void Deposit_PositiveAmount_BalanceIncreases(double balanceInitSum, double sum, double expectedSum)
        {
            IBankAccount bankAccount = new BankAccount(balanceInitSum);
            bankAccount.Deposit(sum);
            var newBalance = bankAccount.GetBalance();
            newBalance.Should().Be(expectedSum);
        }        
    }
}