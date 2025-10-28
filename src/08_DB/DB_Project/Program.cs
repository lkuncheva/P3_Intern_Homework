using Autofac;
using Microsoft.EntityFrameworkCore;
using RPGManager.Configuration;
using RPGManager.Data;
using RPGManager.Interfaces;
using RPGManager.Menus;

namespace RPGManager;

public class Program
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
            Console.WriteLine("4. Character Stats Management");
            Console.WriteLine("5. Character Quests Management");
            Console.WriteLine("6. Character Equipment Management");
            Console.WriteLine("7. Seed Sample Data");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            try
            {
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
                        await CharacterStatsManagementMenuAsync();
                        break;
                    case "5":
                        await CharacterQuestsManagementMenuAsync();
                        break;
                    case "6":
                        await CharacterEquipmentManagementMenuAsync();
                        break;
                    case "7":
                        await SeedSampleDataAsync();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("\nThank you for using Fantasy RPG Character Manager!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
    }

    private static async Task CharacterManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var characterService = scope.Resolve<ICharacterService>();
        var menuHandler = new CharacterManagementMenuHandler(characterService);

        await menuHandler.ShowMenuAsync();
    }

    private static async Task QuestManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var questService = scope.Resolve<IQuestService>();
        var menuHandler = new QuestManagementMenuHandler(questService);

        await menuHandler.ShowMenuAsync();
    }

    private static async Task EquipmentManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var equipmentService = scope.Resolve<IEquipmentService>();
        var menuHandler = new EquipmentManagementMenuHandler(equipmentService);

        await menuHandler.ShowMenuAsync();
    }

    private static async Task CharacterStatsManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var characterStatsService = scope.Resolve<ICharacterStatsService>();
        var menuHandler = new CharacterStatsMenuHandler(characterStatsService);

        await menuHandler.ShowMenuAsync();
    }

    private static async Task CharacterQuestsManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var characterQuestService = scope.Resolve<ICharacterQuestService>();
        var menuHandler = new CharacterQuestsMenuHandler(characterQuestService);

        await menuHandler.ShowMenuAsync();
    }

    private static async Task CharacterEquipmentManagementMenuAsync()
    {
        using var scope = _container!.BeginLifetimeScope();
        var characterEquipmentService = scope.Resolve<ICharacterEquipmentService>();
        var menuHandler = new CharacterEquipmentMenuHandler(characterEquipmentService);

        await menuHandler.ShowMenuAsync();
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
}