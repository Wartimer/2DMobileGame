using System;
using Game.Transport;
using JetBrains.Annotations;
using UnityEngine;

namespace Features.AbilitySystem
{
    internal class JumpAbility : IAbility
    {
        private readonly IAbilityItem _abilityItem;
        
        public JumpAbility([NotNull] IAbilityItem abilityItem) =>
            _abilityItem = abilityItem ?? throw new ArgumentNullException(nameof(abilityItem));

        public void Apply(IAbilityActivator activator)
        {
            var rb = activator.GameObjectView.GetComponent<TransportView>().RigidBody;
            var col = activator.GameObjectView.GetComponent<TransportView>().Collider;
            var mask = activator.GameObjectView.GetComponent<TransportView>().Layer;
            if(IsGrounded(col, mask))
                rb.velocity = Vector2.up * activator.JumpHeight;
        }
        
        internal bool IsGrounded(Collider2D collider, LayerMask layerMask)
        {
            float extraHeightText = 0.1f;
            var colliderBounds = collider.bounds;
            RaycastHit2D raycastHit = Physics2D.Raycast(colliderBounds.center, Vector2.down, colliderBounds.extents.y + extraHeightText, layerMask);
            Color rayColor;
            if (raycastHit.collider != null) rayColor = Color.green;
            else rayColor = Color.red;
            
            Debug.DrawRay(colliderBounds.center, Vector2.down * (colliderBounds.extents.y + extraHeightText), rayColor);
            Debug.Log(raycastHit.collider);
            return raycastHit.collider != null;
        }
        
    }
}