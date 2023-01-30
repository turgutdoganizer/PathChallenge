using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Sql;

namespace PathProjectChallenge.Data.Mapping
{

    public partial class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new()
        {
           
        };

        public Dictionary<(Type, string), string> ColumnName => new()
        {
            
        };
    }
}
