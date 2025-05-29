using TmsSolution.Application.Dtos.Milestone;

namespace TmsSolution.Presentation.GraphQL.Types.Milestone
{
    public class MilestoneType : ObjectType<MilestoneOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<MilestoneOutputDto> descriptor)
        {
            descriptor.Name("Milestone").Description("Represents a project milestone, used to group related work by a due date or goal.");

            descriptor.Field(f => f.Id)
                .Type<NonNullType<IdType>>()
                .Description("The unique identifier of the milestone.");

            descriptor.Field(f => f.ProjectId)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the project this milestone belongs to.");

            descriptor.Field(f => f.Name)
                .Type<NonNullType<StringType>>()
                .Description("Name of the milestone.");

            descriptor.Field(f => f.Description)
                .Type<StringType>()
                .Description("Optional description of the milestone.");

            descriptor.Field(f => f.DueDate)
                .Type<DateTimeType>()
                .Description("Optional due date by which the milestone should be completed.");

            descriptor.Field(f => f.CreatedAt)
                .Type<NonNullType<DateTimeType>>()
                .Description("Date and time when the milestone was created.");

            descriptor.Field(f => f.UpdatedAt)
                .Type<NonNullType<DateTimeType>>()
                .Description("Date and time when the milestone was last updated.");

            descriptor.Field(f => f.TestRunsCount)
                .Type<NonNullType<IntType>>()
                .Description("Number of test runs associated with this milestone.");
        }
    }
}
