using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.Tag;

namespace TmsSolution.Application.Interfaces
{
    public interface ITagService
    {
        IQueryable<TagOutputDto> GetAll();
        Task<TagOutputDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(TagCreateDto tagDto);
        Task<bool> UpdateAsync(Guid id, TagUpdateDto tagDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
