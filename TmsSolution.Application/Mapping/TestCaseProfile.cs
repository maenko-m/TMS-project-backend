using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class TestCaseProfile : Profile
    {
        public TestCaseProfile()
        {
            CreateMap<TestCase, TestCaseOutputDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.Defects, opt => opt.MapFrom(src => src.Defects));

            CreateMap<TestCaseCreateDto, TestCase>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Suite, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.Defects, opt => opt.Ignore())
                .ForMember(dest => dest.TestRunTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.TestPlanTestCases, opt => opt.Ignore());

            CreateMap<TestCaseUpdateDto, TestCase>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Suite, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.Defects, opt => opt.Ignore())
                .ForMember(dest => dest.TestRunTestCases, opt => opt.Ignore())
                .ForMember(dest => dest.TestPlanTestCases, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, context) =>
                {
                    if (srcMember == null) return false;

                    if (srcMember is Guid guid && guid == Guid.Empty)
                        return false;

                    return true;
                }));
        }
    }
}
