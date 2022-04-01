namespace Inventory.Items
{
    internal class Item : IItem
    {
        public string Id { get; }
        public ItemInfo Info { get; }

        internal Item(string id, ItemInfo info)
        {
            Id = id;
            Info = info;
        }
    }
}