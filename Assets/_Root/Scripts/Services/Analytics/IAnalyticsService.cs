using System.Collections.Generic;

namespace Services.Analytics
{
    public interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);

        public void SendTransaction(string productId, decimal amount, string currency);
    }
}