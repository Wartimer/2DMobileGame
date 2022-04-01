using Tool;
using UnityEngine;


namespace Game.Transport
{
    internal sealed class WheelsRotator
    {
        private readonly SubscriptionProperty<float> _diff;
        private ISubscriptionProperty<float> _leftMove;
        private ISubscriptionProperty<float> _rightMove;
        
        public WheelsRotator(
            TransportView transportView,
            ISubscriptionProperty<float> leftMove,
            ISubscriptionProperty<float> rightMove)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _diff = new SubscriptionProperty<float>();
            
            transportView.Init(_diff);
            
            _leftMove.SubscribeOnChange(RotateLeft);
            _rightMove.SubscribeOnChange(RotateRight);
        }

        private void Dispose()
        {
            _leftMove.UnSubscribeOnChange(RotateLeft);
            _rightMove.UnSubscribeOnChange(RotateRight);
        }


        private void RotateLeft(float value) =>
            _diff.Value = -value;

        private void RotateRight(float value) =>
            _diff.Value = value;        
    }
}