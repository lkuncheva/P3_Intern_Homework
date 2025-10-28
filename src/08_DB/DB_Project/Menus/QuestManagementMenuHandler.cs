using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Menus;

public class QuestManagementMenuHandler
{
    private readonly IQuestService _questService;

    public QuestManagementMenuHandler(IQuestService questService)
    {
        _questService = questService ?? throw new ArgumentNullException(nameof(questService));
    }

    public async Task ShowMenuAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== Quest Management ===");
            Console.WriteLine("1. Create Quest");
            Console.WriteLine("2. Bulk Insert Quests from JSON");
            Console.WriteLine("3. View All Quests");
            Console.WriteLine("4. Update Quest Rewards");
            Console.WriteLine("5. Delete Quest");
            Console.WriteLine("6. Export Quests to JSON");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await CreateQuestAsync();
                        break;
                    case "2":
                        await BulkInsertQuestsAsync();
                        break;
                    case "3":
                        await ViewAllQuestsAsync();
                        break;
                    case "4":
                        await UpdateQuestRewardsAsync();
                        break;
                    case "5":
                        await DeleteQuestAsync();
                        break;
                    case "6":
                        await ExportQuestsAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
    }

    private async Task CreateQuestAsync()
    {
        Console.Write("\nEnter quest title: ");
        var title = Console.ReadLine();

        Console.Write("Enter quest description: ");
        var description = Console.ReadLine();

        Console.Write("Enter reward gold: ");
        if (!int.TryParse(Console.ReadLine(), out int gold))
        {
            Console.WriteLine("Invalid gold amount.");
            return;
        }

        Console.Write("Enter reward experience: ");
        if (!int.TryParse(Console.ReadLine(), out int exp))
        {
            Console.WriteLine("Invalid experience amount.");
            return;
        }

        Console.Write("Enter required level: ");
        if (!int.TryParse(Console.ReadLine(), out int reqLevel))
        {
            Console.WriteLine("Invalid level.");
            return;
        }

        Console.Write("Enter difficulty (Easy/Medium/Hard/Expert): ");
        var difficulty = Console.ReadLine();

        var quest = new Quest
        {
            Title = title ?? "Unknown Quest",
            Description = description ?? "",
            RewardGold = gold,
            RewardExperience = exp,
            RequiredLevel = reqLevel,
            Difficulty = difficulty ?? "Easy"
        };

        var created = await _questService.CreateQuestAsync(quest);
        Console.WriteLine($"\nQuest created successfully! ID: {created.Id}");
    }

    private async Task BulkInsertQuestsAsync()
    {
        Console.Write("\nEnter JSON file path: ");
        Console.WriteLine("(../../../SampleData/quests.json)");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        await _questService.BulkInsertQuestsFromJsonAsync(filePath);
    }

    private async Task ViewAllQuestsAsync()
    {
        var quests = await _questService.GetAllQuestsAsync();

        Console.WriteLine("\n=== All Quests ===");
        foreach (var quest in quests)
        {
            Console.WriteLine($"ID: {quest.Id}, Title: {quest.Title}, Difficulty: {quest.Difficulty}, Reward: {quest.RewardGold}g / {quest.RewardExperience}xp");
        }
    }

    private async Task UpdateQuestRewardsAsync()
    {
        Console.Write("\nEnter quest ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter new gold reward: ");
        if (!int.TryParse(Console.ReadLine(), out int gold))
        {
            Console.WriteLine("Invalid gold amount.");
            return;
        }

        Console.Write("Enter new experience reward: ");
        if (!int.TryParse(Console.ReadLine(), out int exp))
        {
            Console.WriteLine("Invalid experience amount.");
            return;
        }

        var success = await _questService.UpdateQuestRewardsAsync(id, gold, exp);
        Console.WriteLine(success ? "\nQuest rewards updated successfully!" : "\nQuest not found.");
    }

    private async Task DeleteQuestAsync()
    {
        Console.Write("\nEnter quest ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Are you sure? (yes/no): ");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "yes")
        {
            var success = await _questService.DeleteQuestAsync(id);
            Console.WriteLine(success ? "\nQuest deleted successfully!" : "\nQuest not found.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private async Task ExportQuestsAsync()
    {
        Console.Write("\nEnter output file path: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        Console.Write("Filter by difficulty (leave empty for all): ");
        var difficulty = Console.ReadLine();

        await _questService.ExportQuestsToJsonAsync(filePath, string.IsNullOrWhiteSpace(difficulty) ? null : difficulty);
    }
}