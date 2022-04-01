using System;
using System.Collections.Generic;
using AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesView
    {
        void Display(IEnumerable<IAbilityItem> abilityItems, Action<string> clicked);
        void Clear();
    }
}