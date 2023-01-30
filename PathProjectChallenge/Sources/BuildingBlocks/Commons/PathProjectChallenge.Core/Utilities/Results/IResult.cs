using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathProjectChallenge.Core.Utilities.Results
{
    public interface IResult
    {
        int StatusCode { get; }
        string Message { get; }
    }
}
