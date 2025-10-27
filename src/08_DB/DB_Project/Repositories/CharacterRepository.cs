using RPGManager.Data;
using RPGManager.Models;
using RPGManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RPGManager.Repositories;

public class CharacterRepository : Repository<Character>, ICharacterRepository
{
    public CharacterRepository(RpgDbContext context) : base(context)
    {
    }

    public async Task<Character> GetCharacterWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(c => c.CharacterClass)
            .Include(c => c.CharacterStats)
            .Include(c => c.CharacterEquipment)
                .ThenInclude(ce => ce.Equipment)
            .Include(c => c.CharacterQuests)
                .ThenInclude(cq => cq.Quest)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Character>> GetCharactersByClassAsync(int classId)
    {
        return await _dbSet
            .Include(c => c.CharacterClass)
            .Where(c => c.CharacterClassId == classId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersByLevelRangeAsync(int minLevel, int maxLevel)
    {
        return await _dbSet
            .Include(c => c.CharacterClass)
            .Where(c => c.Level >= minLevel && c.Level <= maxLevel)
            .OrderBy(c => c.Level)
            .ToListAsync();
    }

    public async Task<IEnumerable<Character>> GetActiveCharactersAsync()
    {
        return await _dbSet
            .Include(c => c.CharacterClass)
            .Where(c => c.IsActive)
            .ToListAsync();
    }
}