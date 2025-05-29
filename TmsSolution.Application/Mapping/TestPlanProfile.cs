using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class TestPlanProfile : Profile
    {
        public TestPlanProfile()
        {
            CreateMap<TestPlan, TestPlanOutputDto>()
                .ForMember(dest => dest.TestCases,
                       opt => opt.MapFrom(src => src.TestPlanTestCases
                           .Select(tptc => tptc.TestCase)));

            CreateMap<TestPlanCreateDto, TestPlan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TestPlanTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore());

            CreateMap<TestPlanUpdateDto, TestPlan>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TestPlanTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) =>
                {
                    if (srcMember == null) return false;

                    if (srcMember is Guid guidValue && guidValue == Guid.Empty)
                        return false;

                    return true;
                }));
        }
    }
}
