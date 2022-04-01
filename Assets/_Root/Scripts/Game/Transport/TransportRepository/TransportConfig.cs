using Scripts.Enums;
using UnityEngine;

namespace Game.Car.TransportRepository
{
    [CreateAssetMenu(fileName = nameof(TransportConfig), menuName = "Configs/Transport/" + nameof(TransportConfig))]
    internal class TransportConfig : ScriptableObject
    {
        [field: SerializeField] public TransportType Type { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}