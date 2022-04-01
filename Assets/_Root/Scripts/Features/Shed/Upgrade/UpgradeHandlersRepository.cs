using System.Collections.Generic;
using _Root.Scripts;
using Scripts.Enums;

namespace Shed.Upgrade
{
    internal class UpgradeHandlersRepository
        : Repository<string, IUpgradeHandler, UpgradeItemConfig>, IUpgradeHandlersRepository
    {
        public UpgradeHandlersRepository(IEnumerable<UpgradeItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(UpgradeItemConfig config) =>
            config.Id;

        protected override IUpgradeHandler CreateItem(UpgradeItemConfig config) =>
            config.Type switch
            {
                UpgradeType.Speed => new SpeedUpgradeHandler(config.Value),
                UpgradeType.JumpHeight => new JumpHeightUpgradeHandler(config.Value),
                _ => StubUpgradeHandler.Default
            };
    }
}