using Ordering.Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Core.Utilities.Results
{
    public class PaginationDataResult<T> : Result, IPaginationResult<T>
    {
        public PaginationDataResult(IReadOnlyList<T> data, HttpStatusCode statusCode, int pageNumber, int pageSize) : base(statusCode)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public PaginationDataResult(IReadOnlyList<T> data, HttpStatusCode statusCode, int pageNumber, int pageSize, string message) : base(statusCode, message)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public IReadOnlyList<T> Data { get; }
    }
}
