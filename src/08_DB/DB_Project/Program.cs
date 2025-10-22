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
            Console.WriteLine("3. Equipment Management");
            Console.WriteLine("4. Seed Sample Data");
            Console.WriteLine("5. Exit");
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
                    await EquipmentManagementMenuAsync();
                    break;
                case "4":
                    await SeedSampleDataAsync();
                    break;
                case "5":
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

    // ===============================================
    // CHARACTER MANAGEMENT
    // ===============================================

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
        Console.WriteLine("7. View Character Stats");
        Console.WriteLine("8. Create/Update Character Stats");
        Console.WriteLine("9. Delete Character Stats");
        Console.WriteLine("10. Delete Character");
        Console.WriteLine("11. Export Characters to JSON");
        Console.WriteLine("12. View Character Quests");
        Console.WriteLine("13. Assign Quest to Character");
        Console.WriteLine("14. Update Quest Status");
        Console.WriteLine("15. View Character Equipment");
        Console.WriteLine("16. Assign Equipment to Character");
        Console.WriteLine("17. Toggle Equipment Status");
        Console.WriteLine("0. Back to Main Menu");

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
                    await ViewCharacterStatsAsync(characterService);
                    break;
                case "8":
                    await CreateOrUpdateCharacterStatsAsync(characterService);
                    break;
                case "9":
                    await DeleteCharacterStatsAsync(characterService);
                    break;
                case "10":
                    await DeleteCharacterAsync(characterService);
                    break;
                case "11":
                    await ExportCharactersAsync(characterService);
                    break;
                case "12":
                    await ViewCharacterQuestsAsync(characterService);
                    break;
                case "13":
                    await AssignQuestToCharacterAsync(characterService);
                    break;
                case "14":
                    await UpdateCharacterQuestStatusAsync(characterService);
                    break;
                case "15":
                    await ViewCharacterEquipmentAsync(characterService);
                    break;
                case "16":
                    await AssignEquipmentToCharacterAsync(characterService);
                    break;
                case "17":
                    await ToggleCharacterEquipmentStatusAsync(characterService);
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

        if (character.CharacterEquipment.Any())
        {
            Console.WriteLine($"\nEquipment ({character.CharacterEquipment.Count} items):");
            foreach (var ce in character.CharacterEquipment)
            {
                var equippedStatus = ce.IsEquipped ? "[EQUIPPED]" : "[IN BAG]";
                Console.WriteLine($"  - {equippedStatus} {ce.Equipment?.Name ?? "Unknown"} ({ce.Equipment?.Type ?? ""}, {ce.Equipment?.Rarity ?? ""})");
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

    // ===============================================
    // EQUIPMENT MANAGEMENT
    // ===============================================

    private static async Task EquipmentManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var equipmentService = scope.Resolve<IEquipmentService>();

        Console.WriteLine("\n=== Equipment Management ===");
        Console.WriteLine("1. Create Equipment Item");
        Console.WriteLine("2. Bulk Insert Equipment from JSON");
        Console.WriteLine("3. View All Equipment");
        Console.WriteLine("4. Update Equipment Bonuses");
        Console.WriteLine("5. Delete Equipment Item");
        Console.WriteLine("6. Export Equipment to JSON");
        Console.WriteLine("7. Assign Equipment to Character");
        Console.WriteLine("8. Toggle Character Equipment Status (Equip/Unequip)");
        Console.WriteLine("9. Back to Main Menu");
        Console.Write("\nSelect an option: ");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    await CreateEquipmentAsync(equipmentService);
                    break;
                case "2":
                    await BulkInsertEquipmentFromJsonAsync(equipmentService);
                    break;
                case "3":
                    await ViewAllEquipmentAsync(equipmentService);
                    break;
                case "4":
                    await UpdateEquipmentBonusAsync(equipmentService);
                    break;
                case "5":
                    await DeleteEquipmentAsync(equipmentService);
                    break;
                case "6":
                    await ExportEquipmentToJsonAsync(equipmentService);
                    break;
                case "7":
                    await AssignEquipmentToCharacterAsync(equipmentService);
                    break;
                case "8":
                    await ToggleEquipmentStatusAsync(equipmentService);
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

    private static async Task CreateEquipmentAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter equipment name: ");
        var name = Console.ReadLine();

        Console.Write("Enter equipment type (e.g., Weapon, Armor): ");
        var type = Console.ReadLine();

        Console.Write("Enter attack bonus (default 0): ");
        if (!int.TryParse(Console.ReadLine(), out int attack)) attack = 0;

        Console.Write("Enter defense bonus (default 0): ");
        if (!int.TryParse(Console.ReadLine(), out int defense)) defense = 0;

        Console.Write("Enter rarity (e.g., Common, Rare, Legendary): ");
        var rarity = Console.ReadLine();

        var equipment = new Equipment
        {
            Name = name ?? "Unknown Item",
            Type = type ?? "Misc",
            AttackBonus = attack,
            DefenseBonus = defense,
            Rarity = rarity ?? "Common"
        };

        var created = await equipmentService.CreateEquipmentAsync(equipment);
        Console.WriteLine($"\nEquipment item created successfully! ID: {created.Id}");
    }

    private static async Task BulkInsertEquipmentFromJsonAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter JSON file path for equipment: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        await equipmentService.BulkInsertEquipmentFromJsonAsync(filePath);
    }

    private static async Task ViewAllEquipmentAsync(IEquipmentService equipmentService)
    {
        var equipment = await equipmentService.GetAllEquipmentAsync();

        Console.WriteLine("\n=== All Equipment ===");
        foreach (var item in equipment)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Type: {item.Type}, Rarity: {item.Rarity}, Attack: +{item.AttackBonus}, Defense: +{item.DefenseBonus}");
        }
    }

    private static async Task UpdateEquipmentBonusAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter equipment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter new attack bonus: ");
        if (!int.TryParse(Console.ReadLine(), out int attack))
        {
            Console.WriteLine("Invalid attack bonus.");
            return;
        }

        Console.Write("Enter new defense bonus: ");
        if (!int.TryParse(Console.ReadLine(), out int defense))
        {
            Console.WriteLine("Invalid defense bonus.");
            return;
        }

        var success = await equipmentService.UpdateEquipmentBonusesAsync(id, attack, defense);
        Console.WriteLine(success ? "\nEquipment bonuses updated successfully!" : "\nEquipment not found.");
    }

    private static async Task DeleteEquipmentAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter equipment ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Are you sure? (yes/no): ");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "yes")
        {
            var success = await equipmentService.DeleteEquipmentAsync(id);
            Console.WriteLine(success ? "\nEquipment deleted successfully!" : "\nEquipment not found.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private static async Task ExportEquipmentToJsonAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter output file path: ");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        Console.Write("Filter by rarity (leave empty for all): ");
        var rarity = Console.ReadLine();

        await equipmentService.ExportEquipmentToJsonAsync(filePath, string.IsNullOrWhiteSpace(rarity) ? null : rarity);
    }

    private static async Task AssignEquipmentToCharacterAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter character ID to assign to: ");
        if (!int.TryParse(Console.ReadLine(), out int charId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter equipment ID to assign: ");
        if (!int.TryParse(Console.ReadLine(), out int equipId))
        {
            Console.WriteLine("Invalid equipment ID.");
            return;
        }

        var success = await equipmentService.AssignEquipmentToCharacterAsync(charId, equipId);
        Console.WriteLine(success ? "\nEquipment assigned successfully!" : "\nFailed to assign equipment. Check IDs.");
    }

    private static async Task ToggleEquipmentStatusAsync(IEquipmentService equipmentService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int charId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter equipment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int equipId))
        {
            Console.WriteLine("Invalid equipment ID.");
            return;
        }

        bool? newStatus = await equipmentService.ToggleEquipmentStatusAsync(charId, equipId);

        if (newStatus.HasValue)
        {
            Console.WriteLine(newStatus.Value
                ? "\nEquipment equipped successfully!"
                : "\nEquipment unequipped successfully!");
        }
        else
        {
            Console.WriteLine("\nFailed to find character/equipment link.");
        }
    }

    // ===============================================
    // QUEST MANAGEMENT
    // ===============================================

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

    // CharacterStats Management Methods
    private static async Task ViewCharacterStatsAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var stats = await characterService.GetCharacterStatsAsync(characterId);
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

    private static async Task CreateOrUpdateCharacterStatsAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var existingStats = await characterService.GetCharacterStatsAsync(characterId);
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
                var success = await characterService.UpdateCharacterStatsAsync(characterId, stats);
                Console.WriteLine(success ? "\nCharacter stats updated successfully!" : "\nFailed to update character stats.");
            }
            else
            {
                var createdStats = await characterService.CreateCharacterStatsAsync(characterId, stats);
                Console.WriteLine($"\nCharacter stats created successfully! ID: {createdStats.Id}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task DeleteCharacterStatsAsync(ICharacterService characterService)
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
                var success = await characterService.DeleteCharacterStatsAsync(characterId);
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

    // CharacterQuest Management Methods
    private static async Task ViewCharacterQuestsAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var quests = await characterService.GetCharacterQuestsAsync(characterId);
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

    private static async Task AssignQuestToCharacterAsync(ICharacterService characterService)
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
            var assignment = await characterService.AssignQuestToCharacterAsync(characterId, questId);
            Console.WriteLine($"\nQuest assigned successfully! Assignment ID: {assignment.CharacterId}-{assignment.QuestId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task UpdateCharacterQuestStatusAsync(ICharacterService characterService)
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
            var success = await characterService.UpdateQuestStatusAsync(characterId, questId, status);
            Console.WriteLine(success ? "\nQuest status updated successfully!" : "\nFailed to update quest status. Check character and quest IDs.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // CharacterEquipment Management Methods
    private static async Task ViewCharacterEquipmentAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var equipment = await characterService.GetCharacterEquipmentAsync(characterId);
            if (!equipment.Any())
            {
                Console.WriteLine("No equipment found for this character.");
                return;
            }

            Console.WriteLine($"\n=== Character Equipment ===");
            Console.WriteLine($"Character ID: {characterId}");
            foreach (var eq in equipment)
            {
                var status = eq.IsEquipped ? "[EQUIPPED]" : "[IN BAG]";
                Console.WriteLine($"- Equipment ID: {eq.EquipmentId} {status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task AssignEquipmentToCharacterAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter equipment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int equipmentId))
        {
            Console.WriteLine("Invalid equipment ID.");
            return;
        }

        try
        {
            var assignment = await characterService.AssignEquipmentToCharacterAsync(characterId, equipmentId);
            Console.WriteLine($"\nEquipment assigned successfully! Assignment ID: {assignment.CharacterId}-{assignment.EquipmentId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task ToggleCharacterEquipmentStatusAsync(ICharacterService characterService)
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        Console.Write("Enter equipment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int equipmentId))
        {
            Console.WriteLine("Invalid equipment ID.");
            return;
        }

        try
        {
            var success = await characterService.ToggleEquipmentStatusAsync(characterId, equipmentId);
            if (success)
            {
                Console.WriteLine("\nEquipment status toggled successfully!");
            }
            else
            {
                Console.WriteLine("\nFailed to toggle equipment status. Check character and equipment IDs.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}