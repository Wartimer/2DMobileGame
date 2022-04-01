using System.Collections.Generic;
using _Root.Scripts;

namespace Inventory.Items
{
    internal class ItemsRepository : Repository<string, IItem, ItemConfig>, IItemsRepository
    {
        public ItemsRepository(IEnumerable<ItemConfig> configs) : base(configs){}
        
        protected override string GetKey(ItemConfig config) =>
            config.Id;


        protected override IItem CreateItem(ItemConfig config) =>
            new Item
            (
                config.Id,
                new ItemInfo(config.Title, config.Icon)
            );
    }
}