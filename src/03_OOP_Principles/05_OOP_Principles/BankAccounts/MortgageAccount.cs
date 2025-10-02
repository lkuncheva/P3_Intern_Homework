namespace BankAccounts;

public class MortgageAccount : Account
{
    public MortgageAccount(CustomerType customer, decimal balance, double interestRate)
        : base(customer, balance, interestRate) { }

    public override decimal CalculateInterest(int months)
    {
        if (Customer == CustomerType.Individual)
        {
            int graceMonths = 6;
            if (months <= graceMonths)
            {
                Console.WriteLine($"    [Rule Applied] Individual mortgage grace period ({graceMonths} months) active. Interest is waived.");
                return 0m;
            }

            int effectiveMonths = months - graceMonths;
            Console.WriteLine($"    [Rule Applied] Interest calculated for {effectiveMonths} effective months.");

            return GetBaseInterest(effectiveMonths);
        }
        else
        {
            int halfRateMonths = Math.Min(months, 12);
            int fullRateMonths = months - halfRateMonths;

            decimal interestHalfRate = GetBaseInterest(halfRateMonths) / 2m;
            decimal interestFullRate = GetBaseInterest(fullRateMonths);

            Console.WriteLine($"    [Rule Applied] Company mortgage: {halfRateMonths} months at 1/2 rate, {fullRateMonths} months at full rate.");

            return interestHalfRate + interestFullRate;
        }
    }
}