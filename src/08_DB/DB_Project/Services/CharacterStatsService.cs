using Newtonsoft.Json;
using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Services;
public class CharacterStatsService : ICharacterStatsService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IRepository<CharacterStats> _characterStatsRepository;

    public CharacterStatsService(
        ICharacterRepository characterRepository,
        IRepository<CharacterStats> characterStatsRepository)
    {
        _characterRepository = characterRepository;
        _characterStatsRepository = characterStatsRepository;
    }

    public async Task<CharacterStats> GetCharacterStatsAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        var stats = await _characterStatsRepository.FindAsync(s => s.CharacterId == characterId);
        return stats.FirstOrDefault();
    }

    public async Task<CharacterStats> CreateCharacterStatsAsync(int characterId, CharacterStats stats)
    {
        if (stats == null)
            throw new ArgumentNullException(nameof(stats));

        var character = await _characterRepository.GetByIdAsync(characterId);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        var existingStats = await _characterStatsRepository.FindAsync(s => s.CharacterId == characterId);
        if (existingStats.Any())
            throw new InvalidOperationException($"Character with ID {characterId} already has stats defined.");

        stats.CharacterId = characterId;
        await _characterStatsRepository.AddAsync(stats);
        return stats;
    }

    public async Task<bool> UpdateCharacterStatsAsync(int characterId, CharacterStats stats)
    {
        if (stats == null)
            throw new ArgumentNullException(nameof(stats));

        var existingStats = await _characterStatsRepository.FindAsync(s => s.CharacterId == characterId);
        var statsToUpdate = existingStats.FirstOrDefault();

        if (statsToUpdate == null)
            return false;

        statsToUpdate.Strength = stats.Strength;
        statsToUpdate.Dexterity = stats.Dexterity;
        statsToUpdate.Intelligence = stats.Intelligence;
        statsToUpdate.Constitution = stats.Constitution;
        statsToUpdate.Wisdom = stats.Wisdom;
        statsToUpdate.Charisma = stats.Charisma;

        await _characterStatsRepository.UpdateAsync(statsToUpdate);
        return true;
    }

    public async Task<bool> DeleteCharacterStatsAsync(int characterId)
    {
        var stats = await _characterStatsRepository.FindAsync(s => s.CharacterId == characterId);
        var statsToDelete = stats.FirstOrDefault();

        if (statsToDelete == null)
            return false;

        await _characterStatsRepository.DeleteAsync(statsToDelete);
        return true;
    }

    public async Task BulkInsertCharacterStatsFromJsonAsync(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath))
            throw new ArgumentException("File path cannot be empty.", nameof(jsonFilePath));

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File not found: {jsonFilePath}");

        var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        var characterStats = JsonConvert.DeserializeObject<List<CharacterStats>>(jsonContent);

        if (characterStats == null || !characterStats.Any())
            throw new InvalidOperationException("No character stats found in JSON file.");

        foreach (var stat in characterStats)
        {
            var character = await _characterRepository.GetByIdAsync(stat.CharacterId);
            if (character == null)
                throw new InvalidOperationException($"Character with ID {stat.CharacterId} not found.");

            var existingStats = await _characterStatsRepository.FindAsync(s => s.CharacterId == stat.CharacterId);
            if (existingStats.Any())
                throw new InvalidOperationException($"Character with ID {stat.CharacterId} already has stats defined.");
        }

        await _characterStatsRepository.AddRangeAsync(characterStats);
        Console.WriteLine($"Successfully inserted {characterStats.Count} character stats from {jsonFilePath}");
    }
}