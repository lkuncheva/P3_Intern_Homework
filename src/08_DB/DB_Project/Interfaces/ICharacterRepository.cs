using RPGManager.Models;

namespace RPGManager.Interfaces;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character> GetCharacterWithDetailsAsync(int id);
    Task<IEnumerable<Character>> GetCharactersByClassAsync(int classId);
    Task<IEnumerable<Character>> GetCharactersByLevelRangeAsync(int minLevel, int maxLevel);
    Task<IEnumerable<Character>> GetActiveCharactersAsync();
}