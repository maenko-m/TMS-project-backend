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

namespace TmsSolution.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProjectQuery>()
                .AddTypeExtension<UserQuery>()

                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<ProjectMutation>()
                .AddTypeExtension<UserMutation>()

                .AddType<ProjectType>()
                .AddType<ProjectCreateInputType>()
                .AddType<ProjectUpdateInputType>()
                .AddType<ProjectAccessTypeEnum>()
                .AddType<UserType>()
                .AddType<UserCreateInputType>()
                .AddType<UserUpdateInputType>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();


            builder.Services.AddSingleton<AuditInterceptor>();
            builder.Services.AddDbContext<TmsDbContext>((serviceProvider, options) =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

                options.UseSqlServer(connectionString)
                       .AddInterceptors(auditInterceptor);
            });
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAutoMapper(typeof(ProjectProfile));
            builder.Services.AddAutoMapper(typeof(UserProfile));

            var app = builder.Build();

            app.MapGet("/", () => "go to /graphql");

            app.MapGraphQL("/graphql");

            app.Run();
        }
    }
}
