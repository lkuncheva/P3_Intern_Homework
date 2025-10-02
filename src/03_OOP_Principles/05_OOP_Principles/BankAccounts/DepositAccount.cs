namespace BankAccounts;

public class DepositAccount : Account
{
    public DepositAccount(CustomerType customer, decimal balance, double interestRate)
        : base(customer, balance, interestRate) { }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance)
        {
            throw new ArgumentException("Invalid withdrawal amount or insufficient funds.");
        }

        Balance -= amount;
        Console.WriteLine($"Withdrew {amount:C}. New Balance: {Balance:C}.");
    }

    public override decimal CalculateInterest(int months)
    {
        if (Balance > 0 && Balance < 1000m)
        {
            Console.WriteLine("    [Rule Applied] Deposit account balance is below 1000. Interest is waived.");
            return 0m;
        }

        return GetBaseInterest(months);
    }
}