using TmsSolution.Application.Dtos.Milestone;

namespace TmsSolution.Presentation.GraphQL.Types.Milestone
{
    public class MilestoneUpdateInputType : InputObjectType<MilestoneUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MilestoneUpdateDto> descriptor)
        {
            descriptor.Name("MilestoneUpdateInput").Description("Input type for updating an existing milestone.");

            descriptor.Field(f => f.ProjectId)
                .Type<IdType>()
                .Description("Updated project ID if reassigned.");

            descriptor.Field(f => f.Name)
                .Type<StringType>()
                .Description("Updated name of the milestone.");

            descriptor.Field(f => f.Description)
                .Type<StringType>()
                .Description("Updated description of the milestone.");

            descriptor.Field(f => f.DueDate)
                .Type<DateTimeType>()
                .Description("Updated due date of the milestone.");
        }
    }
}
