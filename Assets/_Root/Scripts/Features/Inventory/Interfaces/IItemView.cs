using UnityEngine.Events;

namespace Inventory.Items
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction clickAction);
    }

}