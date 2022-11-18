﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAsync();

        Task<WalkDifficulty> GetSingleAsync(Guid id);

        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);
    }
}
