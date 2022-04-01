using Scripts.Enums;

namespace Shed.Upgrade
{
    internal interface IUpgradeHandler
    {
        UpgradeType Type { get; }
        void Upgrade(IUpgradable upgradable);
        void Restore(IUpgradable upgradable);
    }
}