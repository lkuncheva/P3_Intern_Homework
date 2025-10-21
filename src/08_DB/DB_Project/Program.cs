using Autofac;
using RPGManager.Configuration;
using RPGManager.Data;
using RPGManager.Models;
using RPGManager.Services;
using Microsoft.EntityFrameworkCore;
using RPGManager.Interfaces;

namespace RPGManager;

class Program
{
    private static IContainer _container;

    static async Task Main(string[] args)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("  Fantasy RPG Character Manager");
        Console.WriteLine("===========================================\n");

        _container = DependencyConfig.Configure();

        await InitializeDatabaseAsync();

        await RunMainMenuAsync();
    }

    private static async Task InitializeDatabaseAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var context = scope.Resolve<RpgDbContext>();

        Console.WriteLine("Initializing database...");
        await context.Database.MigrateAsync();
        Console.WriteLine("Database initialized successfully!\n");
    }

    private static async Task RunMainMenuAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Character Management");
            Console.WriteLine("2. Quest Management");
            Console.WriteLine("3. Seed Sample Data");
            Console.WriteLine("4. Exit");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CharacterManagementMenuAsync();
                    break;
                case "2":
                    await QuestManagementMenuAsync();
                    break;
                case "3":
                    await SeedSampleDataAsync();
                    break;
                case "4":
                    exit = true;
                    Console.WriteLine("\nThank you for using Fantasy RPG Character Manager!");
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
        }
    }

    private static async Task SeedSampleDataAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var seederService = scope.Resolve<IDataSeederService>();

        try
        {
            await seederService.SeedAllSampleDataAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError seeding data: {ex.Message}");
        }
    }

    private static async Task CharacterManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var characterService = scope.Resolve<ICharacterService>();

        Console.WriteLine("\n=== Character Management ===");
        Console.WriteLine("1. Create Character");
        Console.WriteLine("2. Bulk Insert Characters from JSON");
        Console.WriteLine("3. View All Characters");
        Console.WriteLine("4. View Character Details");
        Console.WriteLine("5. Update Character Name");
        Console.WriteLine("6. Update Character Level");
        Console.WriteLine("7. Delete Character");
        Console.WriteLine("8. Export Characters to JSON");
        Console.WriteLine("9. Back to Main Menu");
        Console.Write("\nSelect an option: ");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    await CreateCharacterAsync(characterService);
                    break;
                case "2":
                    await BulkInsertCharactersAsync(characterService);
                    break;
                case "3":
                    await ViewAllCharactersAsync(characterService);
                    break;
                case "4":
                    await ViewCharacterDetailsAsync(characterService);
                    break;
                case "5":
                    await UpdateCharacterNameAsync(characterService);
                    break;
                case "6":
                    await UpdateCharacterLevelAsync(characterService);
                    break;
                case "7":
                    await DeleteCharacterAsync(characterService);
                    break;
                case "8":
                    await ExportCharactersAsync(characterService);
                    break;
                case "9":
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

    private static async Task CreateCharacterAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character name: ");
        var name = Console.ReadLine();

        Console.Write("Enter character class ID: ");
        if (!int.TryParse(Console.ReadLine(), out int classId))
        {
            Console.WriteLine("Invalid class ID.");
            return;
        }

        Console.Write("Enter starting level (default 1): ");
        var levelInput = Console.ReadLine();
        int level = string.IsNullOrWhiteSpace(levelInput) ? 1 : int.Parse(levelInput);

        var character = new Character
        {
            Name = name ?? "Unknown",
            CharacterClassId = classId,
            Level = level,
            IsActive = true
        };

        var created = await characterService.CreateCharacterAsync(character);
        Console.WriteLine($"\nCharacter created successfully! ID: {created.Id}");
    }

    private static async Task BulkInsertCharactersAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter JSON file path: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        await characterService.BulkInsertCharactersFromJsonAsync(filePath);
    }

    private static async Task ViewAllCharactersAsync(ICharacterService characterService)
    {
        var characters = await characterService.GetAllCharactersAsync();

        Console.WriteLine("\n=== All Characters ===");
        foreach (var character in characters)
        {
            Console.WriteLine($"ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Active: {character.IsActive}");
        }
    }

    private static async Task ViewCharacterDetailsAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var character = await characterService.GetCharacterWithDetailsAsync(id);
        if (character == null)
        {
            Console.WriteLine("Character not found.");
            return;
        }

        Console.WriteLine($"\n=== Character Details ===");
        Console.WriteLine($"ID: {character.Id}");
        Console.WriteLine($"Name: {character.Name}");
        Console.WriteLine($"Level: {character.Level}");
        Console.WriteLine($"Experience: {character.Experience}");
        Console.WriteLine($"Gold: {character.Gold}");
        Console.WriteLine($"Class: {character.CharacterClass?.Name ?? "Unknown"}");
        Console.WriteLine($"Active: {character.IsActive}");
        Console.WriteLine($"Created: {character.CreatedDate}");

        if (character.CharacterStats != null)
        {
            Console.WriteLine($"\nStats:");
            Console.WriteLine($"  Strength: {character.CharacterStats.Strength}");
            Console.WriteLine($"  Dexterity: {character.CharacterStats.Dexterity}");
            Console.WriteLine($"  Intelligence: {character.CharacterStats.Intelligence}");
            Console.WriteLine($"  Constitution: {character.CharacterStats.Constitution}");
            Console.WriteLine($"  Wisdom: {character.CharacterStats.Wisdom}");
            Console.WriteLine($"  Charisma: {character.CharacterStats.Charisma}");
        }

        if (character.Equipment.Any())
        {
            Console.WriteLine($"\nEquipment ({character.Equipment.Count} items):");
            foreach (var item in character.Equipment)
            {
                Console.WriteLine($"  - {item.Name} ({item.Type}, {item.Rarity})");
            }
        }

        if (character.CharacterQuests.Any())
        {
            Console.WriteLine($"\nQuests ({character.CharacterQuests.Count} quests):");
            foreach (var cq in character.CharacterQuests)
            {
                Console.WriteLine($"  - {cq.Quest?.Title ?? "Unknown"} (Status: {cq.Status})");
            }
        }
    }

    private static async Task UpdateCharacterNameAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter new name: ");
        var newName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Invalid name.");
            return;
        }

        var success = await characterService.UpdateCharacterNameAsync(id, newName);
        Console.WriteLine(success ? "\nCharacter name updated successfully!" : "\nCharacter not found.");
    }

    private static async Task UpdateCharacterLevelAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter new level: ");
        if (!int.TryParse(Console.ReadLine(), out int newLevel))
        {
            Console.WriteLine("Invalid level.");
            return;
        }

        var success = await characterService.UpdateCharacterLevelAsync(id, newLevel);
        Console.WriteLine(success ? "\nCharacter level updated successfully!" : "\nCharacter not found.");
    }

    private static async Task DeleteCharacterAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Are you sure? (yes/no): ");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "yes")
        {
            var success = await characterService.DeleteCharacterAsync(id);
            Console.WriteLine(success ? "\nCharacter deleted successfully!" : "\nCharacter not found.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private static async Task ExportCharactersAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter output file path: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        Console.Write("Filter by minimum level (leave empty for no filter): ");
        var minLevelInput = Console.ReadLine();
        int? minLevel = string.IsNullOrWhiteSpace(minLevelInput) ? null : int.Parse(minLevelInput);

        Console.Write("Filter by maximum level (leave empty for no filter): ");
        var maxLevelInput = Console.ReadLine();
        int? maxLevel = string.IsNullOrWhiteSpace(maxLevelInput) ? null : int.Parse(maxLevelInput);

        await characterService.ExportCharactersToJsonAsync(filePath, minLevel, maxLevel);
    }

    private static async Task QuestManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var questService = scope.Resolve<IQuestService>();

        Console.WriteLine("\n=== Quest Management ===");
        Console.WriteLine("1. Create Quest");
        Console.WriteLine("2. Bulk Insert Quests from JSON");
        Console.WriteLine("3. View All Quests");
        Console.WriteLine("4. Update Quest Rewards");
        Console.WriteLine("5. Delete Quest");
        Console.WriteLine("6. Export Quests to JSON");
        Console.WriteLine("7. Assign Quest to Character");
        Console.WriteLine("8. Update Quest Status");
        Console.WriteLine("9. Back to Main Menu");
        Console.Write("\nSelect an option: ");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    await CreateQuestAsync(questService);
                    break;
                case "2":
                    await BulkInsertQuestsAsync(questService);
                    break;
                case "3":
                    await ViewAllQuestsAsync(questService);
                    break;
                case "4":
                    await UpdateQuestRewardsAsync(questService);
                    break;
                case "5":
                    await DeleteQuestAsync(questService);
                    break;
                case "6":
                    await ExportQuestsAsync(questService);
                    break;
                case "7":
                    await AssignQuestToCharacterAsync(questService);
                    break;
                case "8":
                    await UpdateQuestStatusAsync(questService);
                    break;
                case "9":
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

    private static async Task CreateQuestAsync(IQuestService questService)
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

        var created = await questService.CreateQuestAsync(quest);
        Console.WriteLine($"\nQuest created successfully! ID: {created.Id}");
    }

    private static async Task BulkInsertQuestsAsync(IQuestService questService)
    {
        Console.Write("\nEnter JSON file path: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        await questService.BulkInsertQuestsFromJsonAsync(filePath);
    }

    private static async Task ViewAllQuestsAsync(IQuestService questService)
    {
        var quests = await questService.GetAllQuestsAsync();

        Console.WriteLine("\n=== All Quests ===");
        foreach (var quest in quests)
        {
            Console.WriteLine($"ID: {quest.Id}, Title: {quest.Title}, Difficulty: {quest.Difficulty}, Reward: {quest.RewardGold}g / {quest.RewardExperience}xp");
        }
    }

    private static async Task UpdateQuestRewardsAsync(IQuestService questService)
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

        var success = await questService.UpdateQuestRewardsAsync(id, gold, exp);
        Console.WriteLine(success ? "\nQuest rewards updated successfully!" : "\nQuest not found.");
    }

    private static async Task DeleteQuestAsync(IQuestService questService)
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
            var success = await questService.DeleteQuestAsync(id);
            Console.WriteLine(success ? "\nQuest deleted successfully!" : "\nQuest not found.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private static async Task ExportQuestsAsync(IQuestService questService)
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

        await questService.ExportQuestsToJsonAsync(filePath, string.IsNullOrWhiteSpace(difficulty) ? null : difficulty);
    }

    private static async Task AssignQuestToCharacterAsync(IQuestService questService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int charId))
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

        var success = await questService.AssignQuestToCharacterAsync(charId, questId);
        Console.WriteLine(success ? "\nQuest assigned successfully!" : "\nFailed to assign quest.");
    }

    private static async Task UpdateQuestStatusAsync(IQuestService questService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int charId))
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

        Console.Write("Enter new status (NotStarted/InProgress/Completed/Failed): ");
        var status = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(status))
        {
            Console.WriteLine("Invalid status.");
            return;
        }

        var success = await questService.UpdateQuestStatusAsync(charId, questId, status);
        Console.WriteLine(success ? "\nQuest status updated successfully!" : "\nFailed to update quest status.");
    }
}