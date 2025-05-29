using TmsSolution.Application.Dtos.Milestone;

namespace TmsSolution.Presentation.GraphQL.Types.Milestone
{
    public class MilestoneCreateInputType : InputObjectType<MilestoneCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MilestoneCreateDto> descriptor)
        {
            descriptor.Name("MilestoneCreateInput").Description("Input type for creating a new milestone.");

            descriptor.Field(f => f.ProjectId)
                .Type<NonNullType<IdType>>()
                .Description("ID of the project this milestone belongs to.");

            descriptor.Field(f => f.Name)
                .Type<NonNullType<StringType>>()
                .Description("Name of the milestone.");

            descriptor.Field(f => f.Description)
                .Type<StringType>()
                .Description("Optional description for the milestone.");

            descriptor.Field(f => f.DueDate)
                .Type<DateTimeType>()
                .Description("Optional due date of the milestone.");
        }
    }
}
