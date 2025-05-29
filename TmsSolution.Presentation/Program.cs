using TmsSolution.Infrastructure.Data;
using TmsSolution.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using TmsSolution.Infrastructure.Data.Repositories;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Mapping;
using TmsSolution.Application.Services;
using TmsSolution.Presentation.GraphQL.Queries;
using TmsSolution.Presentation.GraphQL.Mutations;
using TmsSolution.Presentation.GraphQL.Types.Project;
using TmsSolution.Presentation.GraphQL.Types.User;
using TmsSolution.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TmsSolution.Presentation.GraphQL.Types.TestSuite;
using TmsSolution.Presentation.GraphQL.Types.TestCase;
using TmsSolution.Presentation.GraphQL.Types.TestStep;
using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.GraphQL.Types.Tag;
using TmsSolution.Presentation.GraphQL.Types.Defect;
using TmsSolution.Presentation.GraphQL.Scalar;
using TmsSolution.Infrastructure.Data.Interfaces;
using TmsSolution.Presentation.GraphQL.Types.TestPlan;
using TmsSolution.Presentation.GraphQL.Types.Milestone;

namespace TmsSolution.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://0.0.0.0:7265");

            builder.Services.AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProjectQuery>()
                .AddTypeExtension<UserQuery>()
                .AddTypeExtension<TestSuiteQuery>()
                .AddTypeExtension<TestCaseQuery>()
                .AddTypeExtension<TagQuery>()
                .AddTypeExtension<DefectQuery>()
                .AddTypeExtension<TestStepQuery>()
                .AddTypeExtension<MilestoneQuery>()
                .AddTypeExtension<TestPlanQuery>()

                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ProjectMutation>()
                .AddTypeExtension<UserMutation>()
                .AddTypeExtension<TestSuiteMutation>()
                .AddTypeExtension<TestCaseMutation>()
                .AddTypeExtension<TagMutation>()
                .AddTypeExtension<DefectMutation>()
                .AddTypeExtension<TestStepMutation>()
                .AddTypeExtension<MilestoneMutation>()
                .AddTypeExtension<TestPlanMutation>()

                .AddType<ProjectType>()
                .AddType<ProjectCreateInputType>()
                .AddType<ProjectUpdateInputType>()
                .AddType<ProjectAccessTypeEnum>()
                .AddType<ProjectFilterInputType>()
                .AddType<ProjectInvolvementTypeEnum>()

                .AddType<UserType>()
                .AddType<UserCreateInputType>()
                .AddType<UserUpdateInputType>()

                .AddType<TestSuiteType>()
                .AddType<TestSuiteCreateInputType>()
                .AddType<TestSuiteUpdateInputType>()
                .AddType<TestCaseStatusType>()
                .AddType<TestCasePriorityType>()
                .AddType<TestCaseSeverityType>()

                .AddType<TestStepType>()
                .AddType<TestStepCreateInputType>()
                .AddType<TestStepUpdateInputType>()

                .AddType<TestCaseType>()
                .AddType<TestCaseCreateInputType>()
                .AddType<TestCaseUpdateInputType>()

                .AddType<TestPlanType>()
                .AddType<TestPlanCreateInputType>()
                .AddType<TestPlanUpdateInputType>()

                .AddType<TagType>()
                .AddType<TagCreateInputType>()
                .AddType<TagUpdateInputType>()

                .AddType<DefectType>()
                .AddType<DefectCreateInputType>()
                .AddType<DefectUpdateInputType>()

                .AddType<MilestoneType>()
                .AddType<MilestoneCreateInputType>()
                .AddType<MilestoneUpdateInputType>()

                .AddType<EnumType<TestCasePriority>>()
                .AddType<EnumType<TestCaseSeverity>>()
                .AddType<EnumType<TestCaseStatus>>()

                .AddType<CustomDateTimeType>()

                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddAuthorization();


            builder.Services.AddSingleton<AuditInterceptor>();
            builder.Services.AddDbContext<TmsDbContext>((serviceProvider, options) =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

                options.UseSqlServer(connectionString)
                       .AddInterceptors(auditInterceptor);
            });

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            var jwtSettings = builder.Configuration
                .GetSection("Jwt")
                .Get<JwtSettings>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings!.Issuer,
                        ValidAudience = jwtSettings!.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITestSuiteRepository, TestSuiteRepository>();
            builder.Services.AddScoped<ITestCaseRepository, TestCaseRepository>();
            builder.Services.AddScoped<ITestStepRepository, TestStepRepository>();
            builder.Services.AddScoped<IDefectRepository, DefectRepository>();
            builder.Services.AddScoped<ITestPlanRepository, TestPlanRepository>();
            builder.Services.AddScoped<IMilestoneRepository, MilestoneRepository>();
            builder.Services.AddScoped<ITestRunTestCaseRepository, TestRunTestCaseRepository>();
            builder.Services.AddScoped<ITestRunRepository, TestRunRepository>();

            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITestSuiteService, TestSuiteService>();
            builder.Services.AddScoped<ITestCaseService, TestCaseService>();
            builder.Services.AddScoped<IDefectService, DefectService>();
            builder.Services.AddScoped<ITestStepService, TestStepService>();
            builder.Services.AddScoped<IMilestoneService, MilestoneService>();
            builder.Services.AddScoped<ITestRunTestCaseService, TestRunTestCaseService>();
            builder.Services.AddScoped<ITestRunService, TestRunService>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            builder.Services.AddAutoMapper(typeof(ProjectProfile));
            builder.Services.AddAutoMapper(typeof(UserProfile));
            builder.Services.AddAutoMapper(typeof(TestSuiteProfile));
            builder.Services.AddAutoMapper(typeof(TagProfile));
            builder.Services.AddAutoMapper(typeof(DefectProfile));
            builder.Services.AddAutoMapper(typeof(TestStepProfile));
            builder.Services.AddAutoMapper(typeof(TestCaseProfile));
            builder.Services.AddAutoMapper(typeof(MilestoneProfile));
            builder.Services.AddAutoMapper(typeof(TestPlanProfile));
            builder.Services.AddAutoMapper(typeof(TestRunTestCaseProfile));
            builder.Services.AddAutoMapper(typeof(TestRunProfile));


            var app = builder.Build();

            app.MapGet("/", () => "go to /graphql");

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers(); 
            app.MapGraphQL("/graphql");
            


            app.Run();
        }
    }
}
