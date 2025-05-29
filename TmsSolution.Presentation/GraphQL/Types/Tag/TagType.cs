using TmsSolution.Application.Dtos.Tag;

namespace TmsSolution.Presentation.GraphQL.Types.Tag
{
    public class TagType : ObjectType<TagOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TagOutputDto> descriptor)
        {
            descriptor.Name("Tag").Description("Represents a tag that can be attached to other entities.");

            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>().Description("Unique identifier of the tag.");
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>().Description("Name of the tag.");
            descriptor.Field(t => t.CreatedAt).Type<NonNullType<DateTimeType>>().Description("Date and time the tag was created.");
        }
    }
}
