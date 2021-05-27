using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaryService.Business.Abstraction
{
    public interface ICrudInterface<TDto>
    {
        Task CreateEntityAsync(TDto model);

        IAsyncEnumerable<TDto> GetEntitiesByPrincipalId(string principalId);

        Task<List<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(int id);

        Task UpdateEntity(TDto model);

        Task DeleteEntityByIdAsync(int id);
    }
}
