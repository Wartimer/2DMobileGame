using System;
using Game.InputLogic;
using Scripts.Enums;
using Tool;
using UnityEngine;

namespace Game.Transport
{
    internal class CarController : TransportController
    {
        private WheelsRotator _wheelsRotator;
        public override GameObject GameObjectView => _view.gameObject;
        
        private BaseInputView _inputView;
        
        
        public CarController(TransportType type, 
                            TransportFactory transportFactory,
                            TransportModel transportModel,
                            SubscriptionProperty<float> leftMove,
                            SubscriptionProperty<float> rightMove,
                            BaseInputView inputView) : base(type, transportFactory, transportModel)
        {
            _wheelsRotator = new WheelsRotator(_view, leftMove, rightMove);
            _inputView = inputView;
            _inputView.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(KeyCode key)
        {
            if (key == KeyCode.UpArrow && IsGrounded())
            {
                _view.RigidBody.velocity = Vector2.up * TransportModel.JumpHeight;
            }
        }

        private bool IsGrounded()
        {
            return _view.IsGrounded();
        }

        protected override void OnDispose()
        {
            _inputView.KeyPressed -= OnKeyPressed;
        }
        

    }
}