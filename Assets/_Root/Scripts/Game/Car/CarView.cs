using UnityEngine;
using Tool;

namespace Game.Car
{
    internal class CarView : MonoBehaviour
    {
        [SerializeField] private Wheel[] _wheels;
        private ISubscriptionProperty<float> _diff;
        
        public void Init(ISubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(RotateWheels);
        }
        
        private void RotateWheels(float value)
        {
            foreach (var wheel in _wheels)
                wheel.Rotate(-value);
        }
    }
}
