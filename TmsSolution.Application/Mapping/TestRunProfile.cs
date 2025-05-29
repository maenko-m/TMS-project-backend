using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class TestRunProfile : Profile
    {
        public TestRunProfile()
        {
            CreateMap<TestRun, TestRunOutputDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.TestRunTestCases, opt => opt.MapFrom(src => src.TestRunTestCases))
                .ForMember(dest => dest.Defects, opt => opt.MapFrom(src => src.Defects));

            CreateMap<TestRunCreateDto, TestRun>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Milestone, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.TestRunTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.Defects, opt => opt.Ignore())
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<TestRunUpdateDto, TestRun>()
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Milestone, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.TestRunTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.Defects, opt => opt.Ignore())
                .ForMember(dest => dest.StartTime, opt => opt.Ignore())
                .ForMember(dest => dest.EndTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, ctx) =>
                {
                    if (srcMember == null) return false;

                    if (srcMember is Guid guid && guid == Guid.Empty)
                        return false;

                    return true;
                }));
        }
    }
}
