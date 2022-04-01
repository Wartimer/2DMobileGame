using UnityEngine;
using UnityEngine.Events;

namespace Features.AbilitySystem
{
    internal interface IAbilityButtonView
    {
        void Init(Sprite icon, UnityAction click);
        void Deinit();
    }
}