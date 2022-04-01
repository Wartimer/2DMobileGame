using Scripts.Enums;

namespace Shed.Upgrade
{
    internal class JumpHeightUpgradeHandler: IUpgradeHandler
    {
        public UpgradeType Type => UpgradeType.JumpHeight;
        private readonly float _jumpHeight;

        public JumpHeightUpgradeHandler(float jumpHeight) =>
            _jumpHeight = jumpHeight;
        
        public void Upgrade(IUpgradable upgradable) =>
            upgradable.JumpHeight += _jumpHeight;
        
        public void Restore(IUpgradable upgradable) =>
            upgradable.JumpHeight -= _jumpHeight;
    }
}