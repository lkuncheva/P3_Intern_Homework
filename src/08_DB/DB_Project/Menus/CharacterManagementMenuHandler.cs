using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Menus;

public class CharacterManagementMenuHandler
{
    private readonly ICharacterService _characterService;

    public CharacterManagementMenuHandler(ICharacterService characterService)
    {
        _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
    }

    public async Task ShowMenuAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== Character Management ===");
            Console.WriteLine("1. Create Character");
            Console.WriteLine("2. Bulk Insert Characters from JSON");
            Console.WriteLine("3. View All Characters");
            Console.WriteLine("4. View Character Details");
            Console.WriteLine("5. Update Character Name");
            Console.WriteLine("6. Update Character Level");
            Console.WriteLine("7. Delete Character");
            Console.WriteLine("8. Export Characters to JSON");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await CreateCharacterAsync();
                        break;
                    case "2":
                        await BulkInsertCharactersAsync();
                        break;
                    case "3":
                        await ViewAllCharactersAsync();
                        break;
                    case "4":
                        await ViewCharacterDetailsAsync();
                        break;
                    case "5":
                        await UpdateCharacterNameAsync();
                        break;
                    case "6":
                        await UpdateCharacterLevelAsync();
                        break;
                    case "7":
                        await DeleteCharacterAsync();
                        break;
                    case "8":
                        await ExportCharactersAsync();
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

    private async Task CreateCharacterAsync()
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

        var created = await _characterService.CreateCharacterAsync(character);
        Console.WriteLine($"\nCharacter created successfully! ID: {created.Id}");
    }

    private async Task BulkInsertCharactersAsync()
    {
        Console.Write("\nEnter JSON file path: ");
        Console.WriteLine("(../../../SampleData/characters.json)");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        await _characterService.BulkInsertCharactersFromJsonAsync(filePath);
    }

    private async Task ViewAllCharactersAsync()
    {
        var characters = await _characterService.GetAllCharactersAsync();

        Console.WriteLine("\n=== All Characters ===");
        foreach (var character in characters)
        {
            Console.WriteLine($"ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Active: {character.IsActive}");
        }
    }

    private async Task ViewCharacterDetailsAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var character = await _characterService.GetCharacterWithDetailsAsync(id);
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

    private async Task UpdateCharacterNameAsync()
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

        var success = await _characterService.UpdateCharacterNameAsync(id, newName);
        Console.WriteLine(success ? "\nCharacter name updated successfully!" : "\nCharacter not found.");
    }

    private async Task UpdateCharacterLevelAsync()
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

        var success = await _characterService.UpdateCharacterLevelAsync(id, newLevel);
        Console.WriteLine(success ? "\nCharacter level updated successfully!" : "\nCharacter not found.");
    }

    private async Task DeleteCharacterAsync()
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
            var success = await _characterService.DeleteCharacterAsync(id);
            Console.WriteLine(success ? "\nCharacter deleted successfully!" : "\nCharacter not found.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private async Task ExportCharactersAsync()
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

        await _characterService.ExportCharactersToJsonAsync(filePath, minLevel, maxLevel);
    }
}