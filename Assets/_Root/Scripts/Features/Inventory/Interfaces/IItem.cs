namespace Inventory.Items
{
    internal interface IItem
    {
        string Id { get; }
        ItemInfo Info { get; }
    }
}