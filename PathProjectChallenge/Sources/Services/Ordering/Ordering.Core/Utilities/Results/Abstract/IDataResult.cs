﻿namespace Ordering.Core.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
        int TotalRecords { get; }
    }
}
