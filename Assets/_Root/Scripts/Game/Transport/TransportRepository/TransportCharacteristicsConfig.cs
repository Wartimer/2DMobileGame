using Scripts.Enums;
using UnityEngine;

namespace Game.Car.TransportRepository
{
    [CreateAssetMenu(fileName = nameof(TransportCharacteristicsConfig), menuName = "Configs/Transport/" + nameof(TransportCharacteristicsConfig))]
    internal class TransportCharacteristicsConfig : ScriptableObject
    {
            [field: SerializeField] public string Title { get; private set; }
            [field: SerializeField] public TransportType Type { get; private set; }
            [field: SerializeField] public float Speed { get; private set; }
            [field: SerializeField] public float JumpHeight { get; private set; }
    }
}