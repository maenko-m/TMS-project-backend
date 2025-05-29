using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.User;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserOutputDto>()
                .ForMember(dto => dto.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHasher.Hash(src.Password)))
                .ForMember(dest => dest.IconBase64, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => UserRole.Regular))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.OwnedProjects, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectUsers, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedSharedSteps, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTestRuns, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTestRunTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDefects, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedAttachments, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedTestPlans, opt => opt.Ignore());

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Password));
                    opt.MapFrom(src => PasswordHasher.Hash(src.Password!));
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
