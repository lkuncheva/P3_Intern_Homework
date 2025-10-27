using Newtonsoft.Json;
using RPGManager.Models;
using RPGManager.Interfaces;

namespace RPGManager.Services;

public class DataSeederService : IDataSeederService
{
    private readonly IRepository<CharacterClass> _characterClassRepository;
    private readonly ICharacterService _characterService;
    private readonly IQuestService _questService;
    private readonly IEquipmentService _equipmentService;

    public DataSeederService(
        IRepository<CharacterClass> characterClassRepository,
        ICharacterService characterService,
        IQuestService questService,
        IEquipmentService equipmentService)
    {
        _characterClassRepository = characterClassRepository ?? throw new ArgumentNullException(nameof(characterClassRepository));
        _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
        _questService = questService ?? throw new ArgumentNullException(nameof(questService));
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
    }

    private string ResolveSampleFilePath(string fileName)
    {
        var candidate = Path.Combine(AppContext.BaseDirectory, "SampleData", fileName);
        if (File.Exists(candidate)) return candidate;

        candidate = Path.Combine(Directory.GetCurrentDirectory(), "SampleData", fileName);
        if (File.Exists(candidate)) return candidate;

        candidate = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "SampleData", fileName));
        if (File.Exists(candidate)) return candidate;

        return Path.Combine(AppContext.BaseDirectory, "SampleData", fileName);
    }

    public async Task SeedCharacterClassesAsync()
    {
        var existingClasses = await _characterClassRepository.GetAllAsync();
        if (existingClasses.Any())
        {
            Console.WriteLine("Character classes already exist. Skipping seed.");
            return;
        }

        var filePath = ResolveSampleFilePath("character_classes.json");
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Sample data file not found: {filePath}");
            return;
        }

        var jsonContent = await File.ReadAllTextAsync(filePath);
        var classes = JsonConvert.DeserializeObject<System.Collections.Generic.List<CharacterClass>>(jsonContent);

        if (classes == null || !classes.Any())
        {
            Console.WriteLine("No character classes found in JSON file.");
            return;
        }

        await _characterClassRepository.AddRangeAsync(classes);
        Console.WriteLine($"Successfully seeded {classes.Count} character classes.");
    }

    public async Task SeedAllSampleDataAsync()
    {
        Console.WriteLine("\n=== Seeding Sample Data ===");

        await SeedCharacterClassesAsync();

        try
        {
            var characterFilePath =ResolveSampleFilePath("characters.json");
            if (File.Exists(characterFilePath))
            {
                await _characterService.BulkInsertCharactersFromJsonAsync(characterFilePath);
            }
            else
            {
                Console.WriteLine($"Characters file not found: {characterFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding characters: {ex.Message}");
            if (ex.InnerException != null) Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.WriteLine($"Base exception: {ex.GetBaseException().Message}");
            Console.WriteLine(ex);
        }

        try
        {
            var questFilePath = ResolveSampleFilePath("quests.json");
            if (File.Exists(questFilePath))
            {
                await _questService.BulkInsertQuestsFromJsonAsync(questFilePath);
            }
            else
            {
                Console.WriteLine($"Quests file not found: {questFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding quests: {ex.Message}");
            if (ex.InnerException != null) Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.WriteLine($"Base exception: {ex.GetBaseException().Message}");
            Console.WriteLine(ex);
        }

        try
        {
            var equipmentFilePath = ResolveSampleFilePath("equipment.json");
            if (File.Exists(equipmentFilePath))
            {
                await _equipmentService.BulkInsertEquipmentFromJsonAsync(equipmentFilePath);
            }
            else
            {
                Console.WriteLine($"Equipment file not found: {equipmentFilePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding equipment: {ex.Message}");
            if (ex.InnerException != null) Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.WriteLine($"Base exception: {ex.GetBaseException().Message}");
            Console.WriteLine(ex);
        }

        Console.WriteLine("=== Sample Data Seeding Complete ===\n");
    }
}