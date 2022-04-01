using System;
using System.Collections.Generic;
using Scripts.Enums;
using Services.Analytics.UnityAnalytics;
using UnityEngine;


namespace Services.Analytics
{
    internal class AnalyticsManager: MonoBehaviour
    {
        public event Action IsInitialized;
        
        [field: SerializeField] public IAnalyticsService[] _services { get; private set; }
        
        private ProfilePlayer _profilePlayer;
        
        internal bool Initialized { get; private set; }
        
        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        
        public void SendGameStarted()
        {
            SendEvent("StageStarted", new Dictionary<string, object>()
            {
                {"CarType", _profilePlayer.CurrentTransport.Type.ToString()},
                {"TimeStamp", DateTime.Now.ToString("hh:mm:ss")}  
            });
            Logging("Stage started event sent");
        }
        
        public void SendTransaction(string productId, decimal amount, string currency)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendTransaction(productId, amount, currency);

            Logging($"Sent transaction {productId}");
        }
        
        private void SendEvent(string eventName)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEvent(eventName);
        }

        private void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _services)
                service.SendEvent(eventName, eventData);
        }

        internal void AnalyticsInit(ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
            Initialized = true;
            IsInitialized?.Invoke();
        }

        private void Logging(string message)
        {
            print($"[{GetType().Name}]: {message}");
        }
    }
}