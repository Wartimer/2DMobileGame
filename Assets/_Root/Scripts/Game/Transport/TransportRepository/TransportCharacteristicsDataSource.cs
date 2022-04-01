using System;
using System.Linq;
using Scripts.Enums;
using UnityEngine;

namespace Game.Car.TransportRepository
{
    [CreateAssetMenu(fileName = nameof(TransportCharacteristicsDataSource), menuName = "Configs/Transport/" + nameof(TransportCharacteristicsDataSource))]
    internal sealed class TransportCharacteristicsDataSource : ScriptableObject
    {
        [System.Serializable]
        private struct TransportCharacteristicsInfo
        {
            public TransportType Type;
            public TransportCharacteristicsConfig CharacteristicsConfig;
        }
        
        [SerializeField] private TransportCharacteristicsInfo[] _transportCharacteristicsInfos;

        public TransportCharacteristicsConfig GetTransportCharacteristics(TransportType type)
        {
            var transportCharInfo = _transportCharacteristicsInfos.FirstOrDefault(info => info.Type == type);
            if (transportCharInfo.CharacteristicsConfig == null)
            {
                throw new InvalidOperationException($"Transport characteristics type {type} not found");
            }

            return transportCharInfo.CharacteristicsConfig;
        }
    }
}