using Newtonsoft.Json;
using RPGManager.Models;
using RPGManager.Interfaces;

namespace RPGManager.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IRepository<Equipment> _equipmentRepository;
    private readonly IRepository<CharacterEquipment> _characterEquipmentRepository;
    private readonly ICharacterRepository _characterRepository;

    public EquipmentService(
        IRepository<Equipment> equipmentRepository,
        IRepository<CharacterEquipment> characterEquipmentRepository,
        ICharacterRepository characterRepository)
    {
        _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        _characterEquipmentRepository = characterEquipmentRepository ?? throw new ArgumentNullException(nameof(characterEquipmentRepository));
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment)
    {
        if (equipment == null)
            throw new ArgumentNullException(nameof(equipment));

        if (string.IsNullOrWhiteSpace(equipment.Name))
            throw new ArgumentException("Equipment name cannot be empty.", nameof(equipment));

        await _equipmentRepository.AddAsync(equipment);
        return equipment;
    }

    public async Task BulkInsertEquipmentFromJsonAsync(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath))
            throw new ArgumentException("File path cannot be empty.", nameof(jsonFilePath));

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File not found: {jsonFilePath}");

        var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        var equipment = JsonConvert.DeserializeObject<List<Equipment>>(jsonContent);

        if (equipment == null || !equipment.Any())
            throw new InvalidOperationException("No equipment found in JSON file.");

        await _equipmentRepository.AddRangeAsync(equipment);
        Console.WriteLine($"Successfully inserted {equipment.Count} equipment items from {jsonFilePath}");
    }

    public async Task<Equipment> GetEquipmentByIdAsync(int id)
    {
        return await _equipmentRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
    {
        return await _equipmentRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentByRarityAsync(string rarity)
    {
        return await _equipmentRepository.FindAsync(q => q.Rarity == rarity);
    }

    public async Task ExportEquipmentToJsonAsync(string outputFilePath, string rarity = null)
    {
        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("Output file path cannot be empty.", nameof(outputFilePath));

        IEnumerable<Equipment> equipment;
        if (!string.IsNullOrWhiteSpace(rarity))
            equipment = await GetEquipmentByRarityAsync(rarity);
        else
            equipment = await GetAllEquipmentAsync();

        var jsonContent = JsonConvert.SerializeObject(equipment, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        await File.WriteAllTextAsync(outputFilePath, jsonContent);
        Console.WriteLine($"Successfully exported {equipment.Count()} equipment items to {outputFilePath}");
    }

    public async Task<Equipment> UpdateEquipmentAsync(Equipment equipment)
    {
        if (equipment == null)
            throw new ArgumentNullException(nameof(equipment));

        var existingEquipment = await _equipmentRepository.GetByIdAsync(equipment.Id);
        if (existingEquipment == null)
            throw new InvalidOperationException($"Equipment with ID {equipment.Id} not found.");

        await _equipmentRepository.UpdateAsync(equipment);
        return equipment;
    }

    public async Task<bool> UpdateEquipmentBonusesAsync(int equipmentId, int newAttackBonus, int newDefenceBonus)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);
        if (equipment == null)
            return false;

        equipment.AttackBonus = newAttackBonus;
        equipment.DefenseBonus = newDefenceBonus;
        await _equipmentRepository.UpdateAsync(equipment);
        return true;
    }

    public async Task<bool> DeleteEquipmentAsync(int equipmentId)
    {
        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);
        if (equipment == null)
            return false;

        await _equipmentRepository.DeleteAsync(equipment);
        return true;
    }

    public async Task<bool> AssignEquipmentToCharacterAsync(int characterId, int equipmentId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

        if (character == null || equipment == null)
            return false;

        var existingAssignment = await _characterEquipmentRepository.FindAsync(
            cq => cq.CharacterId == characterId && cq.EquipmentId == equipmentId);

        if (existingAssignment.Any())
            return false;

        var characterEquipment = new CharacterEquipment
        {
            CharacterId = characterId,
            EquipmentId = equipmentId,
            IsEquipped = false
        };

        await _characterEquipmentRepository.AddAsync(characterEquipment);
        return true;
    }

    public async Task<bool?> ToggleEquipmentStatusAsync(int characterId, int equipmentId)
    {
        var characterEquipmentItems = await _characterEquipmentRepository.FindAsync(
            cq => cq.CharacterId == characterId && cq.EquipmentId == equipmentId);

        var characterEquipment = characterEquipmentItems.FirstOrDefault();
        if (characterEquipment == null)
            return null;

        characterEquipment.IsEquipped = !characterEquipment.IsEquipped;

        await _characterEquipmentRepository.UpdateAsync(characterEquipment);
        return characterEquipment.IsEquipped;
    }

    public async Task<IEnumerable<Equipment>> GetCharacterEquipmentAsync(int characterId)
    {
        var characterEquipment = await _characterEquipmentRepository.FindAsync(cq => cq.CharacterId == characterId);
        var equipmentIds = characterEquipment.Select(cq => cq.EquipmentId).ToList();

        var allQuests = await _equipmentRepository.GetAllAsync();
        return allQuests.Where(q => equipmentIds.Contains(q.Id));
    }
}