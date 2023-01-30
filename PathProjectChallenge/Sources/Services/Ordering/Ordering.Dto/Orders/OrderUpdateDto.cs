using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Dto.Orders
{
    public class OrderUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
