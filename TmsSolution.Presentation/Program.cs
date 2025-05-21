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

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddAutoMapper(typeof(ProjectProfile));
            builder.Services.AddAutoMapper(typeof(UserProfile));

            
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
