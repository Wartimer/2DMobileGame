using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem
{
    internal class GunAbility : IAbility
    {
        private readonly IAbilityItem _abilityItem;
        private Vector3 _forceDirection = new Vector3(1,.3f, 0);
        
        public GunAbility([NotNull] IAbilityItem abilityItem) =>
            _abilityItem = abilityItem ?? throw new ArgumentNullException(nameof(abilityItem));


        public void Apply(IAbilityActivator activator)
        {
            var playerPos = activator.GameObjectView.transform.position;
            var spawnPosition = new Vector2(playerPos.x,
                playerPos.y + 1);
            var projectile = Object.Instantiate(_abilityItem.Projectile, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
            projectile.velocity = _forceDirection * _abilityItem.Value;
        }
        
    }
}