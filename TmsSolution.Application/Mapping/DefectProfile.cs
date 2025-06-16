using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class DefectProfile : Profile
    {
        public DefectProfile()
        {
            CreateMap<Defect, DefectOutputDto>();

            CreateMap<DefectCreateDto, Defect>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.TestRun, opt => opt.Ignore())
                .ForMember(dest => dest.TestCase, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.CustomFields, opt => opt.Ignore());

            CreateMap<DefectUpdateDto, Defect>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.TestRun, opt => opt.Ignore())
                .ForMember(dest => dest.TestCase, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore())
                .ForMember(dest => dest.CustomFields, opt => opt.Ignore())
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
