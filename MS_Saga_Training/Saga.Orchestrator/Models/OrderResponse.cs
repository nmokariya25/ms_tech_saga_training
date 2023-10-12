﻿namespace Saga.Orchestrator.Models
{
    public class OrderResponse
    {
        public string OrderId { get; set; }
        public bool Success { get; internal set; }
        public string Reason { get; internal set; }
    }
}
