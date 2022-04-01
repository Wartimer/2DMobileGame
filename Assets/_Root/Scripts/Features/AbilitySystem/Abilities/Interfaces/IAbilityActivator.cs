using Game.Transport;
using UnityEngine;

namespace Features.AbilitySystem
{
    public interface IAbilityActivator
    {
        float JumpHeight { get; }
        GameObject GameObjectView { get; }
    }
}