using MindTrack.Domain.Entities;
using MindTrack.Domain.Interfaces;
using MindTrack.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MindTrack.Persistence.Repository
{
    public class MoodEntryRepository : IMoodEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public MoodEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MoodEntry> AddAsync(MoodEntry moodEntry)
        {
            _context.MoodEntries.Add(moodEntry);
            await _context.SaveChangesAsync();
            return moodEntry;
        }

        public async Task<MoodEntry> GetByIdAsync(int id)
        {
            return await _context.MoodEntries.FindAsync(id);
        }

        public async Task<IEnumerable<MoodEntry>> GetByUserIdAsync(string userId)
        {
            return await _context.MoodEntries
                .Where(me => me.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(MoodEntry moodEntry)
        {
            _context.MoodEntries.Update(moodEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.MoodEntries.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
