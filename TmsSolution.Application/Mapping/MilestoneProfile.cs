using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Milestone;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class MilestoneProfile : Profile
    {
        public MilestoneProfile()
        {
            CreateMap<Milestone, MilestoneOutputDto>()
                .ForMember(dto => dto.TestRunsCount, opt => opt.MapFrom(src => src.TestRuns.Count));

            CreateMap<MilestoneCreateDto, Milestone>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.TestRuns, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<MilestoneUpdateDto, Milestone>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.TestRuns, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
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
