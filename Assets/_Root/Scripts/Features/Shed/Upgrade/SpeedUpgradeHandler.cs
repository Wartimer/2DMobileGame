
using Scripts.Enums;

namespace Shed.Upgrade
{
    internal class SpeedUpgradeHandler : IUpgradeHandler
    {
        public UpgradeType Type => UpgradeType.Speed;
        private readonly float _speed;

        public SpeedUpgradeHandler(float speed) =>
            _speed = speed;
        
        public void Upgrade(IUpgradable upgradable) =>
            upgradable.Speed += _speed;

        public void Restore(IUpgradable upgradable) =>
            upgradable.Speed -= _speed;
    }
}