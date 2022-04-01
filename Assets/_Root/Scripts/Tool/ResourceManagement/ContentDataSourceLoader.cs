using System;
using System.Linq;
using Features.AbilitySystem;
using Inventory.Items;
using Shed.Upgrade;

namespace Tool
{
    internal class ContentDataSourceLoader
    {
        public static ItemConfig[] LoadItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<ItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<ItemConfig>() : dataSource.ItemConfigs.ToArray();
        }
        
        public static UpgradeItemConfig[] LoadUpgradeItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<UpgradeItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        }

        public static AbilityItemConfig[] LoadAbilityItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<AbilityItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<AbilityItemConfig>() : dataSource.AbilityConfigs.ToArray();
        }
    }
}