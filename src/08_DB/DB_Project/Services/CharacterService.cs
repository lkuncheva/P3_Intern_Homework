using Newtonsoft.Json;
using RPGManager.Models;
using RPGManager.Interfaces;

namespace RPGManager.Services;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;

    public CharacterService(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository ?? throw new ArgumentNullException(nameof(characterRepository));
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
}