namespace BankAccounts
{
    public abstract class Account : IAccount
    {
        public CustomerType Customer { get; protected set; }
        public decimal Balance { get; protected set; }
        public double InterestRate { get; protected set; }

        protected Account(CustomerType customer, decimal balance, double interestRate)
        {
            Customer = customer;
            Balance = balance;
            InterestRate = interestRate;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }

            Balance += amount;
            Console.WriteLine($"Deposited {amount:C}. New Balance: {Balance:C}.");
        }

        public abstract decimal CalculateInterest(int months);

        protected decimal GetBaseInterest(int months)
        {
            return Math.Round(Balance * (decimal)InterestRate * months, 2);
        }
    }
}