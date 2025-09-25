namespace _04._01
{
    public class GSMCallHistoryTest
    {
        public static void Main()
        {
            GSM[] phones =
            [
                new GSM
                {
                    Model = "Galaxy S23 Ultra",
                    Manufacturer = "Samsung",
                    Price = 1199.99,
                    PhoneBattery = new Battery("5000mAh Li-Po", 250, 40, BatteryType.LiIon),
                    PhoneDisplay = new Display(6.8, 16000000)
                },
                new GSM
                {
                    Model = "Pixel 8 Pro",
                    Manufacturer = "Google",
                    Price = 999.00,
                    Owner = "Peter Pan",
                    PhoneDisplay = new Display(6.7, 16000000)
                },
                new GSM
                {
                    Model = "iPhone 15 Pro Max",
                    Manufacturer = "Apple",
                    Price = 1299.99,
                    Owner = "Bate Goiko",
                    PhoneBattery = new Battery("4422mAh Li-Ion", 220, 36, BatteryType.LiIon),
                },
            ];

            phones[0].AddCall("+359 898 233 045", 120);
            phones[0].AddCall("+44 7936 888 772", 300);
            phones[1].AddCall("555-111-2222", 65);
            phones[1].AddCall("+359 936 916 732", 240);
            phones[2].AddCall("+359 796 898 071", 15);

            const double PricePerMinute = 0.37;

            foreach (var phone in phones)
            {
                Console.WriteLine(phone);

                var longestCall = phone.CallHistory.OrderByDescending(c => c.DurationInSeconds).FirstOrDefault();

                if (longestCall != null)
                {
                    Console.WriteLine($"Removing the longest call to {longestCall.DialedPhoneNumber} (Duration: {longestCall.DurationInSeconds}s)...");
                    phone.DeleteCall(longestCall);
                }

                double newTotalPrice = phone.CallsPrice(PricePerMinute);
                Console.WriteLine("\n--- Call History After Removing Longest Call ---");
                Console.WriteLine(phone);
                Console.WriteLine($"New total cost of calls: ${newTotalPrice:0.00}");

                Console.WriteLine("\n--- Clearing Call History ---");
                phone.ClearCallHistory();
                Console.WriteLine(phone);
                Console.WriteLine("Call history has been cleared.\n");
            }            
        }
    }
}