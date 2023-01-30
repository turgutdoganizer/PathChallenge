using Ordering.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Core.Entities
{
    public class Order : Entity
    {
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrencyId { get; set; }

        [Column(TypeName = "decimal(18,10)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,10)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,10)")]
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Suspend,
        Complete,
        Fail
    }
}
