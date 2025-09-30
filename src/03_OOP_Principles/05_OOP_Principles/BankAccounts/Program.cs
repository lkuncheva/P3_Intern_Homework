using BankAccounts;
public class Program()
{
    public static void Main(string[] args)
    {
        const int calculationPeriodMonths = 18;

        List<Account> accounts = new List<Account>
        {
            new DepositAccount(CustomerType.Individual, 800.00m, 0.05),
            new DepositAccount(CustomerType.Company, 1200.00m, 0.05),
            new LoanAccount(CustomerType.Individual, 5000.00m, 0.06),
            new LoanAccount(CustomerType.Company, 15000.00m, 0.07),
            new MortgageAccount(CustomerType.Individual, 100000.00m, 0.04),
            new MortgageAccount(CustomerType.Company, 50000.00m, 0.05)
        };

        Console.WriteLine($"--- Bank Interest Calculation Test for {calculationPeriodMonths} Months ---\n");

        foreach (var account in accounts)
        {
            Console.WriteLine($"[{account.GetType().Name}] Customer: {account.Customer}");
            Console.WriteLine($"  Initial Balance: {account.Balance:C}, Monthly Rate: {account.InterestRate * 100}%");

            if (account.GetType() == typeof(DepositAccount))
            {
                ((DepositAccount)account).Deposit(200.00m);
            }

            decimal interest = account.CalculateInterest(calculationPeriodMonths);
            Console.WriteLine($"  Calculated Interest: {interest:C}\n");
        }

        Console.WriteLine("--- Testing Specific Account Actions ---");

        DepositAccount individualDeposit = (DepositAccount)accounts[0];
        individualDeposit.Withdraw(100.00m);

        Console.WriteLine($"New interest (18 months): {individualDeposit.CalculateInterest(calculationPeriodMonths):C}\n");
    }
}