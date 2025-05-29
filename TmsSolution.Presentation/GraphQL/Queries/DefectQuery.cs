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
