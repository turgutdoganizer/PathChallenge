using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Ordering.Core.Utilities.Extensions;
using Ordering.Service.Extensions.Pagination.Abstract;

namespace Ordering.Service.Extensions.Pagination
{
    public class PaginationUriService : IPaginationUriService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PaginationUriService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Uri GetPageUri(PaginationQuery paginationQuery)
        {
            var baseUri = httpContextAccessor.GetRequestUri();
            var route = httpContextAccessor.GetRoute();
            var endpoint = new Uri(string.Concat(baseUri, route));
            var queryUri = QueryHelpers.AddQueryString($"{endpoint}", "pageNumber", $"{paginationQuery.PageNumber}");
            queryUri = QueryHelpers.AddQueryString(queryUri, "pageSize", $"{paginationQuery.PageSize}");
            return new Uri(queryUri);
        }
    }
}
