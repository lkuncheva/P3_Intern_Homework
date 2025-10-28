using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Menus;

public class CharacterStatsMenuHandler
{
    private readonly ICharacterStatsService _characterStatsService;

    public CharacterStatsMenuHandler(ICharacterStatsService characterStatsService)
    {
        _characterStatsService = characterStatsService ?? throw new ArgumentNullException(nameof(characterStatsService));
    }

    public async Task ShowMenuAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== Character Stats Management ===");
            Console.WriteLine("1. View Character Stats");
            Console.WriteLine("2. Create/Update Character Stats");
            Console.WriteLine("3. Delete Character Stats");
            Console.WriteLine("4. Bulk Insert Stats from JSON");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await ViewCharacterStatsAsync();
                        break;
                    case "2":
                        await CreateOrUpdateCharacterStatsAsync();
                        break;
                    case "3":
                        await DeleteCharacterStatsAsync();
                        break;
                    case "4":
                        await BulkInsertCharacterStatsAsync();
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

    private async Task ViewCharacterStatsAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var stats = await _characterStatsService.GetCharacterStatsAsync(characterId);
            if (stats == null)
            {
                Console.WriteLine("No stats found for this character.");
                return;
            }

            Console.WriteLine($"\n=== Character Stats ===");
            Console.WriteLine($"Character ID: {stats.CharacterId}");
            Console.WriteLine($"Strength: {stats.Strength}");
            Console.WriteLine($"Dexterity: {stats.Dexterity}");
            Console.WriteLine($"Intelligence: {stats.Intelligence}");
            Console.WriteLine($"Constitution: {stats.Constitution}");
            Console.WriteLine($"Wisdom: {stats.Wisdom}");
            Console.WriteLine($"Charisma: {stats.Charisma}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task CreateOrUpdateCharacterStatsAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var existingStats = await _characterStatsService.GetCharacterStatsAsync(characterId);
            bool isUpdate = existingStats != null;

            Console.WriteLine($"\n{(isUpdate ? "Update" : "Create")} Character Stats");

            Console.Write("Enter Strength (default 10): ");
            var strengthInput = Console.ReadLine();
            int strength = string.IsNullOrWhiteSpace(strengthInput) ? 10 : int.Parse(strengthInput);

            Console.Write("Enter Dexterity (default 10): ");
            var dexterityInput = Console.ReadLine();
            int dexterity = string.IsNullOrWhiteSpace(dexterityInput) ? 10 : int.Parse(dexterityInput);

            Console.Write("Enter Intelligence (default 10): ");
            var intelligenceInput = Console.ReadLine();
            int intelligence = string.IsNullOrWhiteSpace(intelligenceInput) ? 10 : int.Parse(intelligenceInput);

            Console.Write("Enter Constitution (default 10): ");
            var constitutionInput = Console.ReadLine();
            int constitution = string.IsNullOrWhiteSpace(constitutionInput) ? 10 : int.Parse(constitutionInput);

            Console.Write("Enter Wisdom (default 10): ");
            var wisdomInput = Console.ReadLine();
            int wisdom = string.IsNullOrWhiteSpace(wisdomInput) ? 10 : int.Parse(wisdomInput);

            Console.Write("Enter Charisma (default 10): ");
            var charismaInput = Console.ReadLine();
            int charisma = string.IsNullOrWhiteSpace(charismaInput) ? 10 : int.Parse(charismaInput);

            var stats = new CharacterStats
            {
                CharacterId = characterId,
                Strength = strength,
                Dexterity = dexterity,
                Intelligence = intelligence,
                Constitution = constitution,
                Wisdom = wisdom,
                Charisma = charisma
            };

            if (isUpdate)
            {
                var success = await _characterStatsService.UpdateCharacterStatsAsync(characterId, stats);
                Console.WriteLine(success ? "\nCharacter stats updated successfully!" : "\nFailed to update character stats.");
            }
            else
            {
                var createdStats = await _characterStatsService.CreateCharacterStatsAsync(characterId, stats);
                Console.WriteLine($"\nCharacter stats created successfully! ID: {createdStats.Id}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteCharacterStatsAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Are you sure you want to delete this character's stats? (yes/no): ");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "yes")
        {
            try
            {
                var success = await _characterStatsService.DeleteCharacterStatsAsync(characterId);
                Console.WriteLine(success ? "\nCharacter stats deleted successfully!" : "\nCharacter stats not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private async Task BulkInsertCharacterStatsAsync()
    {
        Console.Write("\nEnter JSON file path for character stats: ");
        Console.WriteLine("(../../../SampleData/character_stats.json)");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        try
        {
            await _characterStatsService.BulkInsertCharacterStatsFromJsonAsync(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}