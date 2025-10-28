using RPGManager.Interfaces;

namespace RPGManager.Menus;

public class CharacterEquipmentMenuHandler
{
    private readonly ICharacterEquipmentService _characterEquipmentService;

    public CharacterEquipmentMenuHandler(ICharacterEquipmentService characterEquipmentService)
    {
        _characterEquipmentService = characterEquipmentService ?? throw new ArgumentNullException(nameof(characterEquipmentService));
    }

    public async Task ShowMenuAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== Character Equipment Management ===");
            Console.WriteLine("1. View Character Equipment");
            Console.WriteLine("2. Assign Equipment to Character");
            Console.WriteLine("3. Toggle Equipment Status (Equip/Unequip)");
            Console.WriteLine("4. Remove Equipment from Character");
            Console.WriteLine("5. Bulk Insert Character Equipment from JSON");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await ViewCharacterEquipmentAsync();
                        break;
                    case "2":
                        await AssignEquipmentToCharacterAsync();
                        break;
                    case "3":
                        await ToggleEquipmentStatusAsync();
                        break;
                    case "4":
                        await RemoveEquipmentFromCharacterAsync();
                        break;
                    case "5":
                        await BulkInsertCharacterEquipmentAsync();
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

    private async Task ViewCharacterEquipmentAsync()
    {
        Console.Write("\nEnter character ID: ");
        if (!int.TryParse(Console.ReadLine(), out int characterId))
        {
            Console.WriteLine("Invalid character ID.");
            return;
        }

        try
        {
            var equipment = await _characterEquipmentService.GetCharacterEquipmentAsync(characterId);
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

    private async Task AssignEquipmentToCharacterAsync()
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
            var assignment = await _characterEquipmentService.AssignEquipmentToCharacterAsync(characterId, equipmentId);
            Console.WriteLine($"\nEquipment assigned successfully! Assignment ID: {assignment.CharacterId}-{assignment.EquipmentId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task ToggleEquipmentStatusAsync()
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
            var success = await _characterEquipmentService.ToggleEquipmentStatusAsync(characterId, equipmentId);
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

    private async Task RemoveEquipmentFromCharacterAsync()
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

        Console.Write("Are you sure you want to remove this equipment from the character? (yes/no): ");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "yes")
        {
            try
            {
                var success = await _characterEquipmentService.RemoveEquipmentFromCharacterAsync(characterId, equipmentId);
                Console.WriteLine(success ? "\nEquipment removed successfully!" : "\nFailed to remove equipment. Check character and equipment IDs.");
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

    private async Task BulkInsertCharacterEquipmentAsync()
    {
        Console.Write("\nEnter JSON file path for character equipment: ");
        Console.WriteLine("(../../../SampleData/character_equipment.json)");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        try
        {
            await _characterEquipmentService.BulkInsertCharacterEquipmentFromJsonAsync(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}