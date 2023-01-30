using PathProjectChallenge.Core.Domain.Common;
using PathProjectChallenge.Core;
using Catalog.Core.Domain.Catalog;

namespace Catalog.Core.Domain
{
    public class Product : BaseEntity, ISoftDeletedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get ; set; }
        public int CategoryId { get; set; }

    }
}
