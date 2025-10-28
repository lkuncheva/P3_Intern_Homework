using RPGManager.Interfaces;

namespace RPGManager.Menus;

public class CharacterQuestsMenuHandler
{
    private readonly ICharacterQuestService _characterQuestService;

    public CharacterQuestsMenuHandler(ICharacterQuestService characterQuestService)
    {
        _characterQuestService = characterQuestService ?? throw new ArgumentNullException(nameof(characterQuestService));
    }

    public async Task ShowMenuAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== Character Quests Management ===");
            Console.WriteLine("1. View Character Quests");
            Console.WriteLine("2. Assign Quest to Character");
            Console.WriteLine("3. Update Quest Status");
            Console.WriteLine("4. Remove Quest from Character");
            Console.WriteLine("5. Bulk Insert Character Quests from JSON");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await ViewCharacterQuestsAsync();
                        break;
                    case "2":
                        await AssignQuestToCharacterAsync();
                        break;
                    case "3":
                        await UpdateQuestStatusAsync();
                        break;
                    case "4":
                        await RemoveQuestFromCharacterAsync();
                        break;
                    case "5":
                        await BulkInsertCharacterQuestsAsync();
                        break;
                    case "0":
                        exit = true;
                        break;
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

    private async Task ViewCharacterQuestsAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var quests = await _characterQuestService.GetCharacterQuestsAsync(characterId);
            if (!quests.Any())
            {
                Console.WriteLine("No quests found for this character.");
                return;
            }

            Console.WriteLine($"\n=== Character Quests ===");
            Console.WriteLine($"Character ID: {characterId}");
            foreach (var quest in quests)
            {
                Console.WriteLine($"- Quest ID: {quest.QuestId}, Status: {quest.Status}, Started: {quest.StartedDate:yyyy-MM-dd HH:mm}, Completed: {(quest.CompletedDate.HasValue ? quest.CompletedDate.Value.ToString("yyyy-MM-dd HH:mm") : "Not completed")}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task AssignQuestToCharacterAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter quest ID: ");
        if (!int.TryParse(Console.ReadLine(), out int questId))
        {
            Console.WriteLine("Invalid quest ID.");
            return;
        }

        try
        {
            var assignment = await _characterQuestService.AssignQuestToCharacterAsync(characterId, questId);
            Console.WriteLine($"\nQuest assigned successfully! Assignment ID: {assignment.CharacterId}-{assignment.QuestId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task UpdateQuestStatusAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter quest ID: ");
        if (!int.TryParse(Console.ReadLine(), out int questId))
        {
            Console.WriteLine("Invalid quest ID.");
            return;
        }

        Console.WriteLine("Available statuses: NotStarted, InProgress, Completed, Failed");
        Console.Write("Enter new status: ");
        var status = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(status))
        {
            Console.WriteLine("Invalid status.");
            return;
        }

        try
        {
            var success = await _characterQuestService.UpdateQuestStatusAsync(characterId, questId, status);
            Console.WriteLine(success ? "\nQuest status updated successfully!" : "\nFailed to update quest status. Check character and quest IDs.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task RemoveQuestFromCharacterAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter quest ID: ");
        if (!int.TryParse(Console.ReadLine(), out int questId))
        {
            Console.WriteLine("Invalid quest ID.");
            return;
        }

        Console.Write("Are you sure you want to remove this quest from the character? (yes/no): ");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "yes")
        {
            try
            {
                var success = await _characterQuestService.RemoveQuestFromCharacterAsync(characterId, questId);
                Console.WriteLine(success ? "\nQuest removed successfully!" : "\nFailed to remove quest. Check character and quest IDs.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nOperation cancelled.");
        }
    }

    private async Task BulkInsertCharacterQuestsAsync()
    {
        Console.Write("\nEnter JSON file path for character quests: ");
        Console.WriteLine("(../../../SampleData/character_quests.json)");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        try
        {
            await _characterQuestService.BulkInsertCharacterQuestsFromJsonAsync(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}