using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Attachment;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Mapping
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentOutputDto>();
        }
    }
}
