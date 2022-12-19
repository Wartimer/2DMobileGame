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
            var transportView = activator.GameObjectView.GetComponent<TransportView>();
            var rb = transportView.RigidBody;
            var col = transportView.Collider;
            var mask = transportView.Layer;
            if(IsGrounded(col, mask))
                rb.velocity = Vector2.up * activator.JumpHeight;
        }
        
        internal bool IsGrounded(Collider2D collider, LayerMask layerMask)
        {
            float extraHeightText = 0.1f;
            var colliderBounds = collider.bounds;
            var rayDistance = colliderBounds.extents.y + extraHeightText;
            RaycastHit2D raycastHit = Physics2D.Raycast(colliderBounds.center, Vector2.down, rayDistance, layerMask);
            DrawDebugRay(raycastHit, colliderBounds, rayDistance);
            return raycastHit.collider != null;
        }

        private void DrawDebugRay(RaycastHit2D raycastHit, Bounds colliderBounds, float rayDistance)
        {
            Color rayColor;
            if (raycastHit.collider != null) rayColor = Color.green;
            else rayColor = Color.red;

            Debug.DrawRay(colliderBounds.center, Vector2.down * rayDistance, rayColor);
            Debug.Log(raycastHit.collider);
        }
    }
}