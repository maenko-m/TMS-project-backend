using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class TestSuiteProfile : Profile
    {
        public TestSuiteProfile()
        {
            CreateMap<TestSuite, TestSuiteOutputDto>()
                .ForMember(dest => dest.TestCasesCount, opt => opt.MapFrom(src => src.TestCases.Count));

            CreateMap<TestSuiteCreateDto, TestSuite>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.TestCases, opt => opt.Ignore());

            CreateMap<TestSuiteUpdateDto, TestSuite>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.TestCases, opt => opt.Ignore())
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
