using Newtonsoft.Json;
using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Services;
public class CharacterEquipmentService : ICharacterEquipmentService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IRepository<CharacterEquipment> _characterEquipmentRepository;

    public CharacterEquipmentService(
        ICharacterRepository characterRepository,
        IRepository<CharacterEquipment> characterEquipmentRepository)
    {
        _characterRepository = characterRepository;
        _characterEquipmentRepository = characterEquipmentRepository;
    }

    public async Task<IEnumerable<CharacterEquipment>> GetCharacterEquipmentAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        return await _characterEquipmentRepository.FindAsync(ce => ce.CharacterId == characterId);
    }

    public async Task<CharacterEquipment> AssignEquipmentToCharacterAsync(int characterId, int equipmentId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        var existingAssignment = await _characterEquipmentRepository.FindAsync(ce => ce.CharacterId == characterId && ce.EquipmentId == equipmentId);
        if (existingAssignment.Any())
            throw new InvalidOperationException($"Equipment with ID {equipmentId} is already assigned to character with ID {characterId}.");

        var characterEquipment = new CharacterEquipment
        {
            CharacterId = characterId,
            EquipmentId = equipmentId,
            IsEquipped = false
        };

        await _characterEquipmentRepository.AddAsync(characterEquipment);
        return characterEquipment;
    }

    public async Task<bool> ToggleEquipmentStatusAsync(int characterId, int equipmentId)
    {
        var characterEquipment = await _characterEquipmentRepository.FindAsync(ce => ce.CharacterId == characterId && ce.EquipmentId == equipmentId);
        var equipmentToUpdate = characterEquipment.FirstOrDefault();

        if (equipmentToUpdate == null)
            return false;

        equipmentToUpdate.IsEquipped = !equipmentToUpdate.IsEquipped;
        await _characterEquipmentRepository.UpdateAsync(equipmentToUpdate);
        return true;
    }

    public async Task<bool> RemoveEquipmentFromCharacterAsync(int characterId, int equipmentId)
    {
        var characterEquipment = await _characterEquipmentRepository.FindAsync(ce => ce.CharacterId == characterId && ce.EquipmentId == equipmentId);
        var equipmentToDelete = characterEquipment.FirstOrDefault();

        if (equipmentToDelete == null)
            return false;

        await _characterEquipmentRepository.DeleteAsync(equipmentToDelete);
        return true;
    }

    public async Task BulkInsertCharacterEquipmentFromJsonAsync(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath))
            throw new ArgumentException("File path cannot be empty.", nameof(jsonFilePath));

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File not found: {jsonFilePath}");

        var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        var characterEquipment = JsonConvert.DeserializeObject<List<CharacterEquipment>>(jsonContent);

        if (characterEquipment == null || !characterEquipment.Any())
            throw new InvalidOperationException("No character equipment found in JSON file.");

        foreach (var equipment in characterEquipment)
        {
            var character = await _characterRepository.GetByIdAsync(equipment.CharacterId);
            if (character == null)
                throw new InvalidOperationException($"Character with ID {equipment.CharacterId} not found.");

            var existingAssignment = await _characterEquipmentRepository.FindAsync(ce => ce.CharacterId == equipment.CharacterId && ce.EquipmentId == equipment.EquipmentId);
            if (existingAssignment.Any())
                throw new InvalidOperationException($"Equipment with ID {equipment.EquipmentId} is already assigned to character with ID {equipment.CharacterId}.");
        }

        await _characterEquipmentRepository.AddRangeAsync(characterEquipment);
        Console.WriteLine($"Successfully inserted {characterEquipment.Count} character equipment from {jsonFilePath}");
    }
}