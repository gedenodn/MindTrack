using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindTrack.Domain.Entities;

namespace MindTrack.Domain.Interfaces
{
    public interface IMoodEntryRepository
    {
        Task<MoodEntry> AddAsync(MoodEntry moodEntry);
        Task<MoodEntry> GetByIdAsync(int id);
        Task<IEnumerable<MoodEntry>> GetByUserIdAsync(string userId);
        Task UpdateAsync(MoodEntry moodEntry);
        Task DeleteAsync(int id);
    }
}
