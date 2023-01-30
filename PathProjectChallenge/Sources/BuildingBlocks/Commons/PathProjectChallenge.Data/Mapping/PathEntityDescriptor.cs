using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathProjectChallenge.Data.Mapping
{
    public partial class PathEntityDescriptor
    {
        public PathEntityDescriptor()
        {
            Fields = new List<NopEntityFieldDescriptor>();
        }

        public string EntityName { get; set; }
        public string SchemaName { get; set; }
        public ICollection<NopEntityFieldDescriptor> Fields { get; set; }
    }
}
