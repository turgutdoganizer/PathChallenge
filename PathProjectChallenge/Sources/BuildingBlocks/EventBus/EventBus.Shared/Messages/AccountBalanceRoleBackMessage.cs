using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Shared.Messages
{
    public class AccountBalanceRoleBackMessage : IAccountBalanceRoleBackMessage
    {
        public Guid AccountId { get; set ; }
        public double Balance { get; set; }
    }
}
