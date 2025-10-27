using Newtonsoft.Json;
using RPGManager.Interfaces;
using RPGManager.Models;

namespace RPGManager.Services;
public class CharacterQuestService : ICharacterQuestService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IRepository<CharacterQuest> _characterQuestRepository;

    public CharacterQuestService(
        ICharacterRepository characterRepository,
        IRepository<CharacterQuest> characterQuestRepository)
    {
        _characterRepository = characterRepository;
        _characterQuestRepository = characterQuestRepository;
    }

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

    public async Task BulkInsertCharacterQuestsFromJsonAsync(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath))
            throw new ArgumentException("File path cannot be empty.", nameof(jsonFilePath));

        if (!File.Exists(jsonFilePath))
            throw new FileNotFoundException($"File not found: {jsonFilePath}");

        var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
        var characterQuests = JsonConvert.DeserializeObject<List<CharacterQuest>>(jsonContent);

        if (characterQuests == null || !characterQuests.Any())
            throw new InvalidOperationException("No character quests found in JSON file.");

        foreach (var quest in characterQuests)
        {
            var character = await _characterRepository.GetByIdAsync(quest.CharacterId);
            if (character == null)
                throw new InvalidOperationException($"Character with ID {quest.CharacterId} not found.");

            var existingAssignment = await _characterQuestRepository.FindAsync(cq => cq.CharacterId == quest.CharacterId && cq.QuestId == quest.QuestId);
            if (existingAssignment.Any())
                throw new InvalidOperationException($"Quest with ID {quest.QuestId} is already assigned to character with ID {quest.CharacterId}.");

            if (string.IsNullOrWhiteSpace(quest.Status))
                quest.Status = "NotStarted";

            if (quest.StartedDate == default && quest.Status != "NotStarted")
                quest.StartedDate = DateTime.UtcNow;
        }

        await _characterQuestRepository.AddRangeAsync(characterQuests);
        Console.WriteLine($"Successfully inserted {characterQuests.Count} character quests from {jsonFilePath}");
    }
}