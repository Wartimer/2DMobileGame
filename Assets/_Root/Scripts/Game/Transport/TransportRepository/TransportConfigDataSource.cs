using System;
using System.Linq;
using Scripts.Enums;
using UnityEngine;

namespace Game.Car.TransportRepository
{
    [CreateAssetMenu(fileName = nameof(TransportConfigDataSource), menuName = "Configs/Transport/" + nameof(TransportConfigDataSource))]
    internal class TransportConfigDataSource: ScriptableObject
    {
        [System.Serializable]
        private struct TransportInfo
        {
            public TransportType Type;
            public TransportConfig Config;
        }
        
        [SerializeField] private TransportInfo[] _transportInfos;

        public TransportConfig GetTransport(TransportType type)
        {
            var transportInfo = _transportInfos.FirstOrDefault(info => info.Type == type);
            if (transportInfo.Config == null)
            {
                throw new InvalidOperationException($"Transport type {type} not found");
            }

            return transportInfo.Config;
        }
    }
}