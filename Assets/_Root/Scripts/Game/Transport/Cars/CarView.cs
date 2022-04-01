using System;
using Tool;
using UnityEngine;

namespace Game.Transport
{
    internal sealed class CarView : TransportView
    {
        [SerializeField] private Wheel[] _wheels;
        
        public override void Init(ISubscriptionProperty<float> diff)
        {
            base.Init(diff);
            _diff.SubscribeOnChange(RotateWheels);
        }
        
        private void RotateWheels(float value)
        {
            foreach (var wheel in _wheels)
                wheel.Rotate(-value);
        }
        
    }
}