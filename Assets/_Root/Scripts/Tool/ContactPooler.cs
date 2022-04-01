using UnityEngine;

namespace Tool
{
    internal sealed class ContactPooler
    {
        private ContactPoint2D[] _contactPoint2D = new ContactPoint2D[10];
        private const float _contactPointThreshhold = .6f;
        private int _contactCount;
        private Collider2D _collider2D;

        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }

        public ContactPooler(Collider2D collider2D)
        {
            _collider2D = collider2D;
        }

        public void CheckContacts(float deltaTime)
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;

            _contactCount = _collider2D.GetContacts(_contactPoint2D);

            for (int i = 0; i < _contactCount; i++)
            {
                if (_contactPoint2D[i].normal.y > _contactPointThreshhold) IsGrounded = true;
                if (_contactPoint2D[i].normal.x > _contactPointThreshhold) HasRightContact = true;
                if (_contactPoint2D[i].normal.x < -_contactPointThreshhold) HasLeftContact = true;
            }
        }
    }
}