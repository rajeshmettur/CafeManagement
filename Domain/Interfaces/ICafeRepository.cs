using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICafeRepository
    {
        Task<IEnumerable<Cafe>> GetAllAsync();
        Task<Cafe?> GetByIdAsync(Guid id);
        Task AddAsync(Cafe cafe);
        Task UpdateAsync(Cafe cafe);
        Task DeleteAsync(Guid id);
    }
}