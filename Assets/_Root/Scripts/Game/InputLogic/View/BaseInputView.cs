using System;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        public event Action<KeyCode> KeyPressed;
        
        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;
        protected float _speed;


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _speed = speed;
        }

        protected virtual void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected virtual void OnRightMove(float value) =>
            _rightMove.Value = value;
        
        protected void GetKey(float deltaTime)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                KeyPressed?.Invoke(KeyCode.UpArrow);
        }    
            
    }
}
