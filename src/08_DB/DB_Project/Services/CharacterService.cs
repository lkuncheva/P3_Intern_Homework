using Newtonsoft.Json;
using RPGManager.Models;
using RPGManager.Interfaces;

namespace RPGManager.Services;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IRepository<CharacterStats> _statsRepository;
    private readonly IRepository<CharacterQuest> _characterQuestRepository;
    private readonly IRepository<CharacterEquipment> _characterEquipmentRepository;

    public CharacterService(
        ICharacterRepository characterRepository,
        IRepository<CharacterStats> statsRepository,
        IRepository<CharacterQuest> characterQuestRepository,
        IRepository<CharacterEquipment> characterEquipmentRepository)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
        _statsRepository = statsRepository ?? throw new ArgumentNullException(nameof(statsRepository));
        _characterQuestRepository = characterQuestRepository ?? throw new ArgumentNullException(nameof(characterQuestRepository));
        _characterEquipmentRepository = characterEquipmentRepository ?? throw new ArgumentNullException(nameof(characterEquipmentRepository));
    }

    public async Task<Character> CreateCharacterAsync(Character character)
    {
        if (character == null)
            throw new ArgumentNullException(nameof(character));

        if (string.IsNullOrWhiteSpace(character.Name))
            throw new ArgumentException("Character name cannot be empty.", nameof(character));

        character.CreatedDate = DateTime.UtcNow;
        await _characterRepository.AddAsync(character);

        return character;
    }

    public async Task BulkInsertCharactersFromJsonAsync(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath))
            throw new ArgumentException("File path cannot be empty.", nameof(jsonFilePath));

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File not found: {jsonFilePath}");

        var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        var characters = JsonConvert.DeserializeObject<List<Character>>(jsonContent);

        if (characters == null || !characters.Any())
            throw new InvalidOperationException("No characters found in JSON file.");

        foreach (var character in characters)
        {
            character.CreatedDate = DateTime.UtcNow;
        }

        await _characterRepository.AddRangeAsync(characters);
        Console.WriteLine($"Successfully inserted {characters.Count} characters from {jsonFilePath}");
    }

    public async Task<Character> GetCharacterByIdAsync(int id)
    {
        return await _characterRepository.GetByIdAsync(id);
    }

    public async Task<Character> GetCharacterWithDetailsAsync(int id)
    {
        return await _characterRepository.GetCharacterWithDetailsAsync(id);
    }

    public async Task<IEnumerable<Character>> GetAllCharactersAsync()
    {
        return await _characterRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersByFilterAsync(
        int? minLevel = null,
        int? maxLevel = null,
        int? classId = null,
        bool? isActive = null)
    {
        var characters = await _characterRepository.GetAllAsync();

        if (minLevel.HasValue)
            characters = characters.Where(c => c.Level >= minLevel.Value);

        if (maxLevel.HasValue)
            characters = characters.Where(c => c.Level <= maxLevel.Value);

        if (classId.HasValue)
            characters = characters.Where(c => c.CharacterClassId == classId.Value);

        if (isActive.HasValue)
            characters = characters.Where(c => c.IsActive == isActive.Value);

        return characters.ToList();
    }

    public async Task ExportCharactersToJsonAsync(
        string outputFilePath,
        int? minLevel = null,
        int? maxLevel = null,
        int? classId = null)
    {
        if (string.IsNullOrWhiteSpace(outputFilePath))
            throw new ArgumentException("Output file path cannot be empty.", nameof(outputFilePath));

        var characters = await GetCharactersByFilterAsync(minLevel, maxLevel, classId);

        var jsonContent = JsonConvert.SerializeObject(characters, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        await File.WriteAllTextAsync(outputFilePath, jsonContent);
        Console.WriteLine($"Successfully exported {characters.Count()} characters to {outputFilePath}");
    }

    public async Task<Character> UpdateCharacterAsync(Character character)
    {
        if (character == null)
            throw new ArgumentNullException(nameof(character));

        var existingCharacter = await _characterRepository.GetByIdAsync(character.Id);
        if (existingCharacter == null)
            throw new InvalidOperationException($"Character with ID {character.Id} not found.");

        await _characterRepository.UpdateAsync(character);
        return character;
    }

    public async Task<bool> UpdateCharacterNameAsync(int characterId, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("New name cannot be empty.", nameof(newName));

        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            return false;

        character.Name = newName;
        await _characterRepository.UpdateAsync(character);
        return true;
    }

    public async Task<bool> UpdateCharacterLevelAsync(int characterId, int newLevel)
    {
        if (newLevel < 1)
            throw new ArgumentException("Level must be at least 1.", nameof(newLevel));

        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            return false;

        character.Level = newLevel;
        await _characterRepository.UpdateAsync(character);
        return true;
    }

    public async Task<bool> DeleteCharacterAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            return false;

        await _characterRepository.DeleteAsync(character);
        return true;
    }

    // CharacterStats methods
    public async Task<CharacterStats> GetCharacterStatsAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        var stats = await _statsRepository.FindAsync(s => s.CharacterId == characterId);
        return stats.FirstOrDefault();
    }

    public async Task<CharacterStats> CreateCharacterStatsAsync(int characterId, CharacterStats stats)
    {
        if (stats == null)
            throw new ArgumentNullException(nameof(stats));

        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        var existingStats = await _statsRepository.FindAsync(s => s.CharacterId == characterId);
        if (existingStats.Any())
            throw new InvalidOperationException($"Character with ID {characterId} already has stats defined.");

        stats.CharacterId = characterId;
        await _statsRepository.AddAsync(stats);
        return stats;
    }

    public async Task<bool> UpdateCharacterStatsAsync(int characterId, CharacterStats stats)
    {
        if (stats == null)
            throw new ArgumentNullException(nameof(stats));

        var existingStats = await _statsRepository.FindAsync(s => s.CharacterId == characterId);
        var statsToUpdate = existingStats.FirstOrDefault();

        if (statsToUpdate == null)
            return false;

        statsToUpdate.Strength = stats.Strength;
        statsToUpdate.Dexterity = stats.Dexterity;
        statsToUpdate.Intelligence = stats.Intelligence;
        statsToUpdate.Constitution = stats.Constitution;
        statsToUpdate.Wisdom = stats.Wisdom;
        statsToUpdate.Charisma = stats.Charisma;

        await _statsRepository.UpdateAsync(statsToUpdate);
        return true;
    }

    public async Task<bool> DeleteCharacterStatsAsync(int characterId)
    {
        var stats = await _statsRepository.FindAsync(s => s.CharacterId == characterId);
        var statsToDelete = stats.FirstOrDefault();

        if (statsToDelete == null)
            return false;

        await _statsRepository.DeleteAsync(statsToDelete);
        return true;
    }

    // CharacterQuest methods
    public async Task<IEnumerable<CharacterQuest>> GetCharacterQuestsAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        return await _characterQuestRepository.FindAsync(cq => cq.CharacterId == characterId);
    }

    public async Task<CharacterQuest> AssignQuestToCharacterAsync(int characterId, int questId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        var existingAssignment = await _characterQuestRepository.FindAsync(cq => cq.CharacterId == characterId && cq.QuestId == questId);
        if (existingAssignment.Any())
            throw new InvalidOperationException($"Quest with ID {questId} is already assigned to character with ID {characterId}.");

        var characterQuest = new CharacterQuest
        {
            CharacterId = characterId,
            QuestId = questId,
            Status = "NotStarted",
            StartedDate = DateTime.UtcNow
        };

        await _characterQuestRepository.AddAsync(characterQuest);
        return characterQuest;
    }

    public async Task<bool> UpdateQuestStatusAsync(int characterId, int questId, string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be empty.", nameof(status));

        var validStatuses = new[] { "NotStarted", "InProgress", "Completed", "Failed" };
        if (!validStatuses.Contains(status))
            throw new ArgumentException($"Invalid status. Must be one of: {string.Join(", ", validStatuses)}");

        var characterQuest = await _characterQuestRepository.FindAsync(cq => cq.CharacterId == characterId && cq.QuestId == questId);
        var questToUpdate = characterQuest.FirstOrDefault();

        if (questToUpdate == null)
            return false;

        questToUpdate.Status = status;
        if (status == "Completed" || status == "Failed")
        {
            questToUpdate.CompletedDate = DateTime.UtcNow;
        }

        await _characterQuestRepository.UpdateAsync(questToUpdate);
        return true;
    }

    public async Task<bool> RemoveQuestFromCharacterAsync(int characterId, int questId)
    {
        var characterQuest = await _characterQuestRepository.FindAsync(cq => cq.CharacterId == characterId && cq.QuestId == questId);
        var questToDelete = characterQuest.FirstOrDefault();

        if (questToDelete == null)
            return false;

        await _characterQuestRepository.DeleteAsync(questToDelete);
        return true;
    }

    // CharacterEquipment methods
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
}