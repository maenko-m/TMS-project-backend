using TmsSolution.Application.Dtos.Tag;

namespace TmsSolution.Presentation.GraphQL.Types.Tag
{
    public class TagCreateInputType : InputObjectType<TagCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TagCreateDto> descriptor)
        {
            descriptor.Name("TagCreateInput").Description("Input type for creating a tag.");
            descriptor.Field(t => t.Name).Type<NonNullType<StringType>>().Description("Name of the tag to be created.");
        }
    }
}
