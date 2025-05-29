using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.GraphQL.Scalar;

namespace TmsSolution.Presentation.GraphQL.Types.TestRun
{
    public class TestRunType : ObjectType<TestRunOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TestRunOutputDto> descriptor)
        {
            descriptor.Name("TestRun").Description("Represents a test run.");

            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>().Description("Unique identifier of the test run.");
            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("ID of the related project.");
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>().Description("Name of the test run.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("Description of the test run.");
            descriptor.Field(f => f.Environment).Type<StringType>().Description("Environment where the test run is executed.");
            descriptor.Field(f => f.MilestoneId).Type<IdType>().Description("Optional milestone ID.");
            descriptor.Field(f => f.Status).Type<NonNullType<EnumType<TestRunStatus>>>().Description("Current status of the test run.");
            descriptor.Field(f => f.StartTime).Type<CustomDateTimeType>().Description("Optional start time.");
            descriptor.Field(f => f.EndTime).Type<CustomDateTimeType>().Description("Optional end time.");
            descriptor.Field(f => f.CreatedAt).Type<NonNullType<CustomDateTimeType>>().Description("Creation timestamp.");
            descriptor.Field(f => f.UpdatedAt).Type<NonNullType<CustomDateTimeType>>().Description("Last update timestamp.");

            descriptor.Field(f => f.Tags).Type<ListType<ObjectType<TagOutputDto>>>().Description("Associated tags.");
            descriptor.Field(f => f.TestRunTestCases).Type<ListType<ObjectType<TestRunTestCaseOutputDto>>>().Description("Test cases within the test run.");
            descriptor.Field(f => f.Defects).Type<ListType<ObjectType<DefectOutputDto>>>().Description("Defects linked to the run.");
        }
    }
}
