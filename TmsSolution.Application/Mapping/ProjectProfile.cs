using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectOutputDto>()
                .ForMember(dto => dto.OwnerFullName, opt => opt.MapFrom(src => src.Owner != null ? $"{src.Owner.FirstName} {src.Owner.LastName}" : string.Empty))
                .ForMember(dto => dto.ProjectUsersCount, opt => opt.MapFrom(src => src.ProjectUsers.Count))
                .ForMember(dto => dto.TestCasesCount, opt => opt.MapFrom(src => src.TestCases.Count))
                .ForMember(dto => dto.DefectsCount, opt => opt.MapFrom(src => src.Defects.Count));

            CreateMap<ProjectCreateDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectUsers, opt => opt.MapFrom(src => src.ProjectUserIds != null ? src.ProjectUserIds.Select(id => new ProjectUser { UserId = id }).ToList() : new List<ProjectUser>()))
                .ForMember(dest => dest.TestCases, opt => opt.Ignore())
                .ForMember(dest => dest.TestSuites, opt => opt.Ignore())
                .ForMember(dest => dest.TestRuns, opt => opt.Ignore())
                .ForMember(dest => dest.SharedSteps, opt => opt.Ignore())
                .ForMember(dest => dest.Defects, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.TestPlans, opt => opt.Ignore())
                .ForMember(dest => dest.Milestones, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IconBase64, opt => opt.Ignore()); 

            CreateMap<ProjectUpdateDto, Project>()
                .ForMember(dest => dest.ProjectUsers, opt => opt.MapFrom((src, dest) => src.ProjectUserIds != null ? src.ProjectUserIds.Select(id => new ProjectUser { UserId = id }).ToList() : dest.ProjectUsers))
                .ForMember(dest => dest.IconBase64, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) =>
                {
                    if (srcMember == null)
                        return false;

                    if (srcMember is Guid guidValue && guidValue == Guid.Empty)
                        return false;

                    return true;
                }));
        }
    }
}
