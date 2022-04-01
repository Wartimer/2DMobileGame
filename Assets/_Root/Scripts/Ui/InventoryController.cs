using System;
using Inventory.Items;
using JetBrains.Annotations;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal interface IInventoryController
    {
    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly IItemsRepository _itemsRepository;

        public InventoryController(
            [NotNull] IInventoryView view,
            [NotNull] IInventoryModel inventoryModel,
            [NotNull] IItemsRepository itemsRepository
            )
        {
            _model
                = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
            
            _itemsRepository = itemsRepository ??
                        throw new ArgumentNullException(nameof(itemsRepository));

            _view = view ??
                    throw new ArgumentNullException(nameof(view));

            _view.Display(_itemsRepository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
        }

        protected override void OnDispose()
        {
            _view.Clear();
            base.OnDispose();
        }
        
        private void OnItemClicked(string itemId)
        {
            bool equipped = _model.IsEquipped(itemId);

            if (equipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);
        }
        
        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }
    }
}