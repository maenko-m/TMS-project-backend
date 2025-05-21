using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.User;

namespace TmsSolution.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserOutputDto>> GetAllAsync();
        Task<UserOutputDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(UserCreateDto userDto);
        Task<bool> UpdateAsync(Guid id, UserUpdateDto userDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
