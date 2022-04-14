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
        }
    }
}