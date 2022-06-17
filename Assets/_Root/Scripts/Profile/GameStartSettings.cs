using Scripts.Enums;
using UnityEngine;

namespace Profile.GameSettings
{
    [CreateAssetMenu(fileName = nameof(GameStartSettings), menuName = "Configs/" + nameof(GameStartSettings))]
    internal class GameStartSettings : ScriptableObject
    {
        [field: SerializeField] public GameState GameState { get; private set; } 
    }
}