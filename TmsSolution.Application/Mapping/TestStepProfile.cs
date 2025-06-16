using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class TestStepProfile : Profile
    {
        public TestStepProfile()
        {
            CreateMap<TestStep, TestStepOutputDto>();

            CreateMap<TestStepCreateDto, TestStep>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.TestCase, opt => opt.Ignore());

            CreateMap<TestStepUpdateDto, TestStep>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.TestCase, opt => opt.Ignore())
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
