using System.Collections.Generic;

namespace Inventory.Items
{
    public interface IInventoryModel
    {
        IReadOnlyList<string> EquippedItems { get; }
        void EquipItem(string item);
        void UnequipItem(string itemId);
        bool IsEquipped(string itemId);
    }
}