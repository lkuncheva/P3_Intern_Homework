namespace BankAccounts;

public class LoanAccount : Account
{
    public LoanAccount(CustomerType customer, decimal balance, double interestRate)
        : base(customer, balance, interestRate) { }

    public override decimal CalculateInterest(int months)
    {
        int graceMonths = Customer == CustomerType.Individual ? 3 : 2;

        if (months <= graceMonths)
        {
            Console.WriteLine($"    [Rule Applied] Loan account grace period ({graceMonths} months) active. Interest is waived.");
            return 0m;
        }

        int effectiveMonths = months - graceMonths;
        Console.WriteLine($"    [Rule Applied] Interest calculated for {effectiveMonths} effective months.");

        return GetBaseInterest(effectiveMonths);
    }
}