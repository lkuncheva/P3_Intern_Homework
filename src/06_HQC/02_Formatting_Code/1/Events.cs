using System.Text;

class Event : IComparable<Event>
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }

    public Event(DateTime date, string title, string location)
    {
        this.Date = date;
        this.Title = title;
        this.Location = location;
    }

    public int CompareTo(Event other)
    {
        if (other == null)
        {
            return 1;
        }

        int byDate = this.Date.CompareTo(other.Date);
        if (byDate != 0)
        {
            return byDate;
        }

        int byTitle = this.Title.CompareTo(other.Title);
        if (byTitle != 0)
        {
            return byTitle;
        }

        return this.Location.CompareTo(other.Location);
    }

    public int CompareTo(object obj)
    {
        if (obj is Event other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Object is not an Event");
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        result.Append(Date.ToString("yyyy-MM-ddTHH:mm:ss"));
        result.Append(" | " + Title);

        if (!string.IsNullOrEmpty(Location))
        {
            result.Append(" | " + Location);
        }

        return result.ToString();
    }
}

class Program
{
    static readonly StringBuilder Output = new StringBuilder();
    static readonly EventHolder Events = new EventHolder();

    private static class Messages
    {
        public static void EventAdded()
        {
            Output.Append("Event added\n");
        }

        public static void EventDeleted(int deletedCount)
        {
            if (deletedCount == 0)
            {
                NoEventsFound();
            }
            else
            {
                Output.AppendFormat($"{deletedCount} events deleted\n");
            }
        }

        public static void NoEventsFound()
        {
            Output.Append("No events found\n");
        }

        public static void PrintEvent(Event eventToPrint)
        {
            if (eventToPrint != null)
            {
                Output.Append(eventToPrint + "\n");
            }
        }
    }

    class EventHolder
    {
        private MultiDictionary<string, Event> byTitle = new MultiDictionary<string, Event>(true);
        private OrderedBag<Event> byDate = new OrderedBag<Event>();

        public void AddEvent(DateTime date, string title, string location)
        {
            Event newEvent = new Event(date, title, location);

            byTitle.Add(title.ToLowerInvariant(), newEvent);
            byDate.Add(newEvent);
            Messages.EventAdded();
        }

        public void DeleteEvents(string titleToDelete)
        {
            string titleKey = titleToDelete.ToLowerInvariant();
            int removedCount = 0;

            if (byTitle.ContainsKey(titleKey))
            {
                var eventsToRemove = byTitle[titleKey].ToList();

                foreach (var eventToRemove in eventsToRemove)
                {
                    byDate.Remove(eventToRemove);
                    removedCount++;
                }

                byTitle.Remove(titleKey);
            }

            Messages.EventDeleted(removedCount);
        }

        public void ListEvents(DateTime date, int count)
        {
            Event searchStartEvent = new Event(date, string.Empty, string.Empty);

            OrderedBag<Event>.View eventsToShow = byDate.RangeFrom(searchStartEvent, true);

            int showed = 0;
            foreach (var eventToShow in eventsToShow)
            {
                if (showed >= count)
                {
                    break;
                }

                Messages.PrintEvent(eventToShow);
                showed++;
            }

            if (showed == 0)
            {
                Messages.NoEventsFound();
            }
        }
    }

    static void Main(string[] args)
    {
        while (ExecuteNextCommand()) { }

        Console.WriteLine(Output.ToString());
    }

    private static bool ExecuteNextCommand()
    {
        string command = Console.ReadLine();

        if (string.IsNullOrEmpty(command))
        {
            return false;
        }

        switch (command[0])
        {
            case 'A':
                AddEvent(command);
                return true;
            case 'D':
                DeleteEvents(command);
                return true;
            case 'L':
                ListEvents(command);
                return true;
            case 'E':
                return false;
            default:
                return true;
        }
    }

    private static void ListEvents(string command)
    {
        DateTime date = GetDate(command, "ListEvents");

        int pipeIndex = command.IndexOf('|');
        if (pipeIndex == -1)
        {
            return;
        }

        string countString = command.Substring(pipeIndex + 1).Trim();

        if (int.TryParse(countString, out int count))
        {
            Events.ListEvents(date, count);
        }
    }

    private static void DeleteEvents(string command)
    {
        const string CommandName = "DeleteEvents";
        string title = command.Substring(CommandName.Length + 1).Trim();

        Events.DeleteEvents(title);
    }

    private static void AddEvent(string command)
    {
        DateTime date;
        string title;
        string location;

        GetParameters(command, "AddEvent", out date, out title, out location);

        Events.AddEvent(date, title, location);
    }

    private static void GetParameters(string commandForExecution, string commandType,
        out DateTime dateAndTime, out string eventTitle, out string eventLocation)
    {
        dateAndTime = GetDate(commandForExecution, commandType);

        int dateEndIndex = commandType.Length + 1 + 20;

        int firstPipeIndex = commandForExecution.IndexOf('|', dateEndIndex);
        int lastPipeIndex = commandForExecution.LastIndexOf('|');

        if (firstPipeIndex == -1)
        {
            eventTitle = string.Empty;
            eventLocation = string.Empty;

            return;
        }

        if (firstPipeIndex == lastPipeIndex)
        {
            eventTitle = commandForExecution.Substring(firstPipeIndex + 1).Trim();
            eventLocation = string.Empty;
        }
        else
        {
            eventTitle = commandForExecution.Substring(firstPipeIndex + 1,
                lastPipeIndex - firstPipeIndex - 1).Trim();
            eventLocation = commandForExecution.Substring(lastPipeIndex + 1).Trim();
        }
    }

    private static DateTime GetDate(string command, string commandType)
    {
        int dateStartIndex = commandType.Length + 1;
        string dateString = command.Substring(dateStartIndex, 20);

        DateTime date = DateTime.Parse(dateString);

        return date;
    }
}