using Shed.Upgrade;
using Scripts.Enums;

namespace Game.Transport
{
    internal class TransportModel : IUpgradable
    {
        private float _defaultSpeed;
        private float _defaultJumpHeight;
        
        public TransportType Type { get; private set; }
        
        public  float Speed { get; set; }
        public float JumpHeight { get; set; }

        public TransportModel(){}
        
        public TransportModel(float speed, float jumpHeight)
        {
        }

        internal void SetTransportType(TransportType type)
        {
            Type = type;
        }

        internal void TransportModelInit(float speed, float jumpHeight)
        {
            _defaultSpeed = speed;
            _defaultJumpHeight = jumpHeight;
            Speed = speed;
            JumpHeight = jumpHeight;
        }
        
        public void RestoreSpeed() =>
            Speed = _defaultSpeed;
        
        
    }
}
