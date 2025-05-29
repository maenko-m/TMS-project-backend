using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Infrastructure.Data.Repositories;

namespace TmsSolution.Application.Services
{
    public class TagService : ITagService
    {
        private readonly TagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(TagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public IQueryable<TagOutputDto> GetAll()
        {
            return _tagRepository
                .GetAll()
                .Select(t => new TagOutputDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    CreatedAt = t.CreatedAt,
                });
        }

        public async Task<TagOutputDto> GetByIdAsync(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<TagOutputDto>(tag);
        }

        public async Task<bool> AddAsync(TagCreateDto tagDto)
        {
            Validator.Validate(tagDto);

            var tag = _mapper.Map<Tag>(tagDto);

            return await _tagRepository.AddAsync(tag);
        }
        public async Task<bool> UpdateAsync(Guid id, TagUpdateDto tagDto)
        {
            Validator.Validate(tagDto);

            var tag = await _tagRepository.GetByIdAsync(id);
            _mapper.Map(tagDto, tag);

            return await _tagRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return await _tagRepository.DeleteAsync(tag);
        }

    }
}
