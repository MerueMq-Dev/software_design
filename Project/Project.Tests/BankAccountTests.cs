using FluentAssertions;
using Project.Infrastructure;

namespace Project.Tests
{
    public class BankAccountTests
    {
        [Theory]
        [InlineData(100)]
        [InlineData(0.01)]
        [InlineData(999999)]
        public void Constructor_PositiveBalance_SetsBalanceCorrectly(double initialBalance)
        {
            IBankAccount bankAccount = new BankAccount(initialBalance);
            bankAccount.GetBalance().Should().Be(initialBalance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_ZeroOrNegativeBalance_ThrowsArgumentOutOfRangeException(double initialBalance)
        {
            Action act = () => new BankAccount(initialBalance);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(100, 50, 150)]
        [InlineData(100, 0.01, 100.01)]
        [InlineData(50, 50, 100)]
        public void Deposit_PositiveAmount_BalanceIncreases(double initialBalance, double sum, double expectedBalance)
        {
            IBankAccount bankAccount = new BankAccount(initialBalance);
            bankAccount.Deposit(sum);
            bankAccount.GetBalance().Should().Be(expectedBalance);
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(100, -1)]
        [InlineData(100, -50)]
        public void Deposit_ZeroOrNegativeAmount_ThrowsArgumentOutOfRangeException(double initialBalance, double sum)
        {
            IBankAccount bankAccount = new BankAccount(initialBalance);
            Action act = () => bankAccount.Deposit(sum);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(100, 30, 70)]
        [InlineData(100, 100, 0)]
        public void Withdraw_PositiveAmount_BalanceDecreases(double initialBalance, double sum, double expectedBalance)
        {
            IBankAccount bankAccount = new BankAccount(initialBalance);
            bankAccount.Withdraw(sum);
            bankAccount.GetBalance().Should().Be(expectedBalance);
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(100, -1)]
        [InlineData(100, -50)]
        public void Withdraw_ZeroOrNegativeAmount_ThrowsArgumentOutOfRangeException(double initialBalance, double sum)
        {
            IBankAccount bankAccount = new BankAccount(initialBalance);
            Action act = () => bankAccount.Withdraw(sum);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(100, 150)]
        [InlineData(100, 101)]
        public void Withdraw_InsufficientFunds_ThrowsInvalidOperationException(double initialBalance, double sum)
        {
            IBankAccount bankAccount = new BankAccount(initialBalance);
            Action act = () => bankAccount.Withdraw(sum);
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Deposit_ThenWithdraw_BalanceIsCorrect()
        {
            IBankAccount bankAccount = new BankAccount(100);
            bankAccount.Deposit(50);
            bankAccount.Withdraw(30);
            bankAccount.GetBalance().Should().Be(120);
        }

        [Fact]
        public void Withdraw_MultipleTimes_BalanceDecreasesCorrectly()
        {
            IBankAccount bankAccount = new BankAccount(100);
            bankAccount.Withdraw(20);
            bankAccount.Withdraw(30);
            bankAccount.GetBalance().Should().Be(50);
        }
    }
}