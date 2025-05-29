using TmsSolution.Application.Dtos.Tag;

namespace TmsSolution.Presentation.GraphQL.Types.Tag
{
    public class TagUpdateInputType : InputObjectType<TagUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TagUpdateDto> descriptor)
        {
            descriptor.Name("TagUpdateInput").Description("Input type for updating a tag.");
            descriptor.Field(t => t.Name).Type<StringType>().Description("New name of the tag.");
        }
    }
}
