using Tool;
using UnityEngine;


namespace Game.Car
{
    internal sealed class WheelsInitController : BaseController
    {
        private readonly SubscriptionProperty<float> _diff;
        private readonly ISubscriptionProperty<float> _leftMove;
        private readonly ISubscriptionProperty<float> _rightMove;
        
        public WheelsInitController(
            CarView carView,
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _diff = new SubscriptionProperty<float>();
            
            carView.Init(_diff);
            
            _leftMove.SubscribeOnChange(RotateLeft);
            _rightMove.SubscribeOnChange(RotateRight);
        }

        protected override void OnDispose()
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