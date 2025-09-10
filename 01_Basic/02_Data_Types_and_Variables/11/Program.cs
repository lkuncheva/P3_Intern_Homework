using System;

class HolderName
{
    public string firstName, middleName, lastName;

}

class BankAccount
{

    public HolderName name;
    public decimal balance; 
    public string bankName;
    public string IBAN;
    public string[] cardNumber;

    public override string ToString()
    {

        string cardNumbers = string.Join(",\n\t", cardNumber);

        return "Bank Account Details:\n\n" + 
               $"Account Holder: {name.firstName} {name.middleName} {name.lastName}\n" +
               $"Current Balance in Lev: {balance}\n" +
               $"Bank Name: {bankName}\n" +
               $"IBAN: {IBAN}\n" +
               $"Credit Card Numbers:\n\t{cardNumbers}.";
    }

}
class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount();
        account.name = new HolderName();
        account.name.firstName = "Lia";
        account.name.middleName = "Nikolova";
        account.name.lastName = "Kuncheva";
        account.balance = 457943.96m;
        account.bankName = "Banka DSK";
        account.IBAN = "BG75 STSA 9300 0032 1234 56";
        account.cardNumber = new string[3] { "1234 5678 9101 1123", "9876 5432 1987 6543", "1928 3746 5555 0192" };

        Console.WriteLine(account);
    }
}
