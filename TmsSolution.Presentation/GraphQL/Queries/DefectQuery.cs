using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class DefectQuery
    {
        /// <summary>
        /// Retrieves a paginated, sortable list of all defects in the system with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="defectService">The service used to retrieve defect data.</param>
        /// <param name="filter">Optional filtering criteria for narrowing down results.</param>
        /// <returns>
        /// A queryable collection of <see cref="DefectOutputDto"/> representing the defects.
        /// </returns>
        /// <remarks>
        /// Authorization is required. Only users with the "Admin" role can access this query.
        /// </remarks>
        /// <exception cref="GraphQLException">
        /// Thrown when an error occurs while retrieving defects.
        /// </exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<DefectOutputDto> GetDefects(
            ClaimsPrincipal user,
            [Service] IDefectService defectService,
            DefectFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var defects = defectService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Title))
                    {
                        defects = defects.Where(p => p.Title.Contains(filter.Title));
                    }

                    if (filter.TestCaseId.HasValue)
                    {
                        defects = defects.Where(tc => tc.TestCaseId == filter.TestCaseId.Value);
                    }

                    if (filter.TestRunId.HasValue)
                    {
                        defects = defects.Where(tc => tc.TestRunId == filter.TestRunId.Value);
                    }

                    if (filter.Severity.HasValue)
                    {
                        defects = defects.Where(tc => tc.Severity == filter.Severity.Value);
                    }

                }

                return defects;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a paginated, sortable list of defects associated with a specific project, with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project whose defects are being requested.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="defectService">The service used to retrieve defect data.</param>
        /// <param name="filter">Optional filtering criteria for narrowing down results.</param>
        /// <returns>
        /// A queryable collection of <see cref="DefectOutputDto"/> for the specified project.
        /// </returns>
        /// <remarks>
        /// Authorization is required. Accessible to all authenticated users with permission to the specified project.
        /// </remarks>
        /// <exception cref="GraphQLException">
        /// Thrown when an error occurs while retrieving defects.
        /// </exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<DefectOutputDto> GetDefectsByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] IDefectService defectService,
            DefectFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var defects = defectService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Title))
                    {
                        defects = defects.Where(p => p.Title.Contains(filter.Title));
                    }

                    if (filter.TestCaseId.HasValue)
                    {
                        defects = defects.Where(tc => tc.TestCaseId == filter.TestCaseId.Value);
                    }

                    if (filter.TestRunId.HasValue)
                    {
                        defects = defects.Where(tc => tc.TestRunId == filter.TestRunId.Value);
                    }

                    if (filter.Severity.HasValue)
                    {
                        defects = defects.Where(tc => tc.Severity == filter.Severity.Value);
                    }
                }

                return defects;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a single defect by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the defect to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="defectService">The service used to retrieve the defect.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the <see cref="DefectOutputDto"/>.
        /// </returns>
        /// <remarks>
        /// Authorization is required. Accessible to all authenticated users with permission to the resource.
        /// </remarks>
        /// <exception cref="GraphQLException">
        /// Thrown when the defect cannot be found or access is denied.
        /// </exception>
        [Authorize]
        public async Task<DefectOutputDto> GetDefectById(
            Guid id,
            ClaimsPrincipal user,
            [Service] IDefectService defectService)
        {
            try
            {
                var userId = user.GetUserId();

                return await defectService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
