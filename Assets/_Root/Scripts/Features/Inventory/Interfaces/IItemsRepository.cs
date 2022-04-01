using System.Collections.Generic;

namespace Inventory.Items
{
    internal interface IItemsRepository
    {
        IReadOnlyDictionary<string, IItem> Items { get; }
    }
}