using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Menus;

public class EquipmentManagementMenuHandler
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentManagementMenuHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
    }

    public async Task ShowMenuAsync()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== Equipment Management ===");
            Console.WriteLine("1. Create Equipment Item");
            Console.WriteLine("2. Bulk Insert Equipment from JSON");
            Console.WriteLine("3. View All Equipment");
            Console.WriteLine("4. Update Equipment Bonuses");
            Console.WriteLine("5. Delete Equipment Item");
            Console.WriteLine("6. Export Equipment to JSON");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await CreateEquipmentAsync();
                        break;
                    case "2":
                        await BulkInsertEquipmentFromJsonAsync();
                        break;
                    case "3":
                        await ViewAllEquipmentAsync();
                        break;
                    case "4":
                        await UpdateEquipmentBonusAsync();
                        break;
                    case "5":
                        await DeleteEquipmentAsync();
                        break;
                    case "6":
                        await ExportEquipmentToJsonAsync();
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

    private async Task CreateEquipmentAsync()
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

        var created = await _equipmentService.CreateEquipmentAsync(equipment);
        Console.WriteLine($"\nEquipment item created successfully! ID: {created.Id}");
    }

    private async Task BulkInsertEquipmentFromJsonAsync()
    {
        Console.Write("\nEnter JSON file path for equipment: ");
        Console.WriteLine("(../../../SampleData/equipment.json)");
        var filePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        await _equipmentService.BulkInsertEquipmentFromJsonAsync(filePath);
    }

    private async Task ViewAllEquipmentAsync()
    {
        var equipment = await _equipmentService.GetAllEquipmentAsync();

        Console.WriteLine("\n=== All Equipment ===");
        foreach (var item in equipment)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Type: {item.Type}, Rarity: {item.Rarity}, Attack: +{item.AttackBonus}, Defense: +{item.DefenseBonus}");
        }
    }

    private async Task UpdateEquipmentBonusAsync()
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

        var success = await _equipmentService.UpdateEquipmentBonusesAsync(id, attack, defense);
        Console.WriteLine(success ? "\nEquipment bonuses updated successfully!" : "\nEquipment not found.");
    }

    private async Task DeleteEquipmentAsync()
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
            var success = await _equipmentService.DeleteEquipmentAsync(id);
            Console.WriteLine(success ? "\nEquipment deleted successfully!" : "\nEquipment not found.");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
    }

    private async Task ExportEquipmentToJsonAsync()
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

        await _equipmentService.ExportEquipmentToJsonAsync(filePath, string.IsNullOrWhiteSpace(rarity) ? null : rarity);
    }
}