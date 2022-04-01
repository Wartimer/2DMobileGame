using UnityEngine;
using System.Collections.Generic;

namespace Features.AbilitySystem
{
    [CreateAssetMenu(
        fileName = nameof(AbilityItemConfigDataSource),
        menuName = "Configs/Abilities/" + nameof(AbilityItemConfigDataSource))]
    internal class AbilityItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private AbilityItemConfig[] _abilityConfigs;

        public IReadOnlyList<AbilityItemConfig> AbilityConfigs => _abilityConfigs;
    }
}