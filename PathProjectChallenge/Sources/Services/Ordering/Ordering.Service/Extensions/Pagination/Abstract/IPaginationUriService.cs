namespace Ordering.Service.Extensions.Pagination.Abstract
{
    public interface IPaginationUriService
    {
        public Uri GetPageUri(PaginationQuery paginationQuery);
    }
}
