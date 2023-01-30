using Automatonymous;
using System.ComponentModel.DataAnnotations.Schema;

namespace SagaStateMachine.WorkerService.Models
{
    public class OrderStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string CurrentState { get; set; }

        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid AccountId { get; set; }
        public Guid CurrencyId { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public double TotalPrice { get; set; }

    }
}
