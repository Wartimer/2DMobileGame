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
            TryGetOrAddRigidBody();
            AddOrGetCollider();
        }

        public virtual void Init(ISubscriptionProperty<float> diff) =>
            _diff = diff;

        private void TryGetOrAddRigidBody()
        {
            if (TryGetComponent(out Rigidbody2D rigidBody))
                _rigidbody = rigidBody;
            else
                gameObject.AddComponent<Rigidbody2D>();
        }

        private void AddOrGetCollider()
        {
            if (TryGetComponent(out Collider2D objectCollider))
                _collider = objectCollider;
            else
                gameObject.AddComponent<Collider2D>();
        }
    }
}
