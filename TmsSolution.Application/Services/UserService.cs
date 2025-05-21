using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.User;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Infrastructure.Data.Repositories;

namespace TmsSolution.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IQueryable<UserOutputDto> GetAll()
        {
            return _userRepository.GetAll()
                .Select(u => new UserOutputDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    IconBase64 = u.IconBase64,
                    FullName = u.FirstName + " " + u.LastName
                });
        }

        public async Task<UserOutputDto> GetByIdAsync(Guid id)
        {
            var users = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserOutputDto>(users);
        }

        public async Task<bool> AddAsync(UserCreateDto userDto)
        {
            Validator.Validate(userDto);

            var user = _mapper.Map<User>(userDto);

            if (!string.IsNullOrWhiteSpace(userDto.IconPath))
                user.IconBase64 = await ImageProcessor.ConvertIconToBase64(userDto.IconPath);

            return await _userRepository.AddAsync(user);
        }
        public async Task<bool> UpdateAsync(Guid id, UserUpdateDto userDto)
        {
            Validator.Validate(userDto);

            var user = await _userRepository.GetByIdAsync(id);
            _mapper.Map(userDto, user);

            if (!string.IsNullOrWhiteSpace(userDto.IconPath))
                user.IconBase64 = await ImageProcessor.ConvertIconToBase64(userDto.IconPath);

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
