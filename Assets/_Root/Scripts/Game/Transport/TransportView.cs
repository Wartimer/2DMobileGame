using System;
using UnityEngine;
using Tool;

namespace Game.Transport
{
    internal class TransportView : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        protected ISubscriptionProperty<float> _diff;
        protected Rigidbody2D _rigidbody;
        protected Collider2D _collider;
        
        internal Rigidbody2D RigidBody => _rigidbody;
        internal Collider2D Collider => _collider;

        internal LayerMask Layer => _layerMask;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        public virtual void Init(ISubscriptionProperty<float> diff) =>
            _diff = diff;   
        
        internal bool IsGrounded()
        {
            float extraHeightText = 0.1f;
            var colliderBounds = Collider.bounds;
            RaycastHit2D raycastHit = Physics2D.Raycast(Collider.bounds.center, Vector2.down, Collider.bounds.extents.y + extraHeightText, _layerMask);
            Color rayColor;
            if (raycastHit.collider != null) rayColor = Color.green;
            else rayColor = Color.red;
            
            Debug.DrawRay(Collider.bounds.center, Vector2.down * (Collider.bounds.extents.y + extraHeightText), rayColor);
            Debug.Log(raycastHit.collider);
            return raycastHit.collider != null;
        }
    }
}
