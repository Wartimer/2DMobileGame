using Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Items;
using UnityEngine;
using Shed.Upgrade;
using JetBrains.Annotations;
using Tool;
using Object = UnityEngine.Object;

namespace Ui
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        
        private readonly ResourcePath _upgradeHandlersDataSourcePath = new ResourcePath("Configs/Upgrades/UpgradeItemConfigDataSource");
        private readonly ResourcePath _itemsConfigDataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
        private readonly ResourcePath _invetoryViewPath = new ResourcePath("Prefabs/UI/InventoryView");
        private readonly ResourcePath _shedViewPath = new ResourcePath("Prefabs/UI/ShedView");
        
        private readonly ShedView _shedView;
        private readonly IInventoryView _inventoryView;
        private readonly ProfilePlayer _profilePlayer;
        private readonly IInventoryController _inventoryController;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;
        private readonly List<string> _appliedItems = new List<string>();

        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));
            
            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateUpgradeHandlersRepository();
            AddRepository(_upgradeHandlersRepository);
            
            _inventoryController = CreateInventoryController(placeForUi);
            AddController(_inventoryController as BaseController);

            _shedView = LoadShedView(placeForUi);
            _shedView.Init(Apply, Close, StartGame);
        }

        protected override void OnDispose()
        {
            _shedView.Deinit();
            base.OnDispose();
        }

        private void Apply()
        {
            if (_appliedItems.Count == _profilePlayer.Inventory.EquippedItems.Count)
            {
                _shedView.Print("All Items Applied. Start the game");
                return;
            }
            
            UpgradeWithEquippedItems(
                _profilePlayer.CurrentTransport,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _shedView.Print("Apply. " +
                        $"Current Speed: {_profilePlayer.CurrentTransport.Speed}. " +
                        $"Current Jump Height: {_profilePlayer.CurrentTransport.JumpHeight}");

        }

        private void Close()
        {
            _profilePlayer.CurrentState.Value = GameState.SelectCar;
            Log("Back. " +
                $"Current Speed: {_profilePlayer.CurrentTransport.Speed}. " +
                $"Current Jump Height: {_profilePlayer.CurrentTransport.JumpHeight}");
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            if(_appliedItems.Count < equippedItems.Count)
                foreach (var itemId in equippedItems)
                {
                    if (_appliedItems.Contains(itemId)) continue;
                    if (!upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    {
                        throw new InvalidOperationException($"There is no upgrade handler {itemId}");
                    }
                    handler.Upgrade(upgradable);
                    _appliedItems.Add(itemId);
                }

            if (_appliedItems.Count > equippedItems.Count)
            {
                var temp = _appliedItems.Except(equippedItems).ToArray();

                foreach (var itemId in temp)
                {
                    if (!upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    {
                        throw new InvalidOperationException($"There is no upgrade handler {itemId}");
                    }
                    handler.Restore(upgradable);
                    _appliedItems.Remove(itemId);
                }
            }
            
        }
        
        
        private InventoryController CreateInventoryController(Transform placeForUi) =>
            new InventoryController(LoadInventoryView(placeForUi), _profilePlayer.Inventory, CreateInventoryItemRepository());
    
        private ItemsRepository CreateInventoryItemRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_itemsConfigDataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            return repository;
        }
    
        private IUpgradeHandlersRepository CreateUpgradeHandlersRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_upgradeHandlersDataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            return repository;
        }

        #region LoadViews
        private ShedView LoadShedView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_shedViewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);
            
            return objectView.GetComponent<ShedView>();
        }
    
        private IInventoryView LoadInventoryView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_invetoryViewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);
            
            return objectView.GetComponent<InventoryView>();
        }
        #endregion        

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}]: {message}");
        
    }
}