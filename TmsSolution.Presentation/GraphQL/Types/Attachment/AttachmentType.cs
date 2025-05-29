using TmsSolution.Application.Dtos.Attachment;
using TmsSolution.Presentation.GraphQL.Scalar;

namespace TmsSolution.Presentation.GraphQL.Types.Attachment
{
    public class AttachmentType : ObjectType<AttachmentOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AttachmentOutputDto> descriptor)
        {
            descriptor.Name("Attachment");
            descriptor.Description("Represents a file attached to a project.");

            descriptor.Field(f => f.Id)
                .Type<NonNullType<UuidType>>()
                .Description("The unique identifier of the attachment.");

            descriptor.Field(f => f.ProjectId)
                .Type<NonNullType<UuidType>>()
                .Description("The ID of the project to which the file is attached.");

            descriptor.Field(f => f.FileName)
                .Type<NonNullType<StringType>>()
                .Description("The name of the file, including its extension.");

            descriptor.Field(f => f.FileUrl)
                .Type<NonNullType<StringType>>()
                .Description("The relative path to the file on the server.");

            descriptor.Field(f => f.FileSize)
                .Type<NonNullType<LongType>>()
                .Description("The size of the file in bytes.");

            descriptor.Field(f => f.ContentType)
                .Type<NonNullType<StringType>>()
                .Description("The MIME type of the file content (e.g., image/png, application/pdf).");

            descriptor.Field(f => f.CreatedAt)
                .Type<NonNullType<CustomDateTimeType>>() 
                .Description("The UTC date and time when the file was uploaded.");

            descriptor.Field(f => f.UploadedById)
                .Type<NonNullType<UuidType>>()
                .Description("The ID of the user who uploaded the file.");
        }
    }
}
