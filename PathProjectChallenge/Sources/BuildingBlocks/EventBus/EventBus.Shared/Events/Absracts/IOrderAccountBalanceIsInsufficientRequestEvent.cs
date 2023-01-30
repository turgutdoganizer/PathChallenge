﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Shared.Events.Absracts
{
    public interface IOrderAccountBalanceIsInsufficientRequestEvent
    {
        public Guid OrderId { get; set; }

        public string Reason { get; set; }
    }
}
