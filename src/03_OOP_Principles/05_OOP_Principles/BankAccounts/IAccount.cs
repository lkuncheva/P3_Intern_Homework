namespace BankAccounts
{
    public interface IAccount
    {
        void Deposit(decimal amount);
        decimal CalculateInterest(int months);
    }
}