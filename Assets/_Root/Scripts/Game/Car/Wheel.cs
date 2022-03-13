using UnityEngine;

namespace Game.Car
{
    public class Wheel: MonoBehaviour
    {
        [SerializeField] private float _relativeSpeed;
        
        public void Rotate(float value)
        {
            transform.Rotate(0,0,value * _relativeSpeed);
        }
    }
}