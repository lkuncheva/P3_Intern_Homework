using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _04._01
{
    public class Call
    {
        private DateTime dateTime;
        private string dialedPhoneNumber;
        private string durationInSeconds;

        public DateTime DateTime { get; set; }
        public string DialedPhoneNumber { get; set; }
        public int DurationInSeconds { get; set; }

        public Call(string phoneNumber, int duration)
        {
            this.DateTime = DateTime.Now;
            this.DialedPhoneNumber = phoneNumber;
            this.DurationInSeconds = duration;
        }

        public override string ToString()
        {
            TimeSpan duration = TimeSpan.FromSeconds(this.DurationInSeconds);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("------------------------------------");
            sb.AppendLine($"Date: {this.DateTime.ToShortDateString()}");
            sb.AppendLine($"Time: {this.DateTime.ToShortTimeString()}");
            sb.AppendLine($"Number: {this.DialedPhoneNumber}");
            sb.AppendLine($"Duration: {duration.ToString(@"mm\:ss")}");
            sb.AppendLine("------------------------------------");
            return sb.ToString();
        }
    }
}
