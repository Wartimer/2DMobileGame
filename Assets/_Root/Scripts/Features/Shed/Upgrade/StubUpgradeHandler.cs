using Scripts.Enums;

namespace Shed.Upgrade
{
    internal class StubUpgradeHandler : IUpgradeHandler
    {
        public UpgradeType Type => UpgradeType.None;
        
        internal static readonly IUpgradeHandler Default = new StubUpgradeHandler();

        public void Upgrade(IUpgradable upgradable){}
        
        public void Restore(IUpgradable upgradable){}
    }
}