using System.Collections.Generic;
using Tool;

namespace Inventory.Items
{
    public class InventoryModel : IInventoryModel
    {
        private readonly List<string> _equippedItems = new List<string>();
        
        public IReadOnlyList<string> EquippedItems => _equippedItems;
        
        public void EquipItem(string itemId)
        {
            if (!IsEquipped(itemId))
            {
                _equippedItems.Add(itemId);
            }
        }

        public void UnequipItem(string itemId)
        {
            if (IsEquipped(itemId))
            {
                _equippedItems.Remove(itemId);
            }
        }
        
        

        public bool IsEquipped(string itemId) =>
            _equippedItems.Contains(itemId);
        
    }
}