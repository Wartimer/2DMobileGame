using Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/UI/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Upgrades/UpgradeItemConfigDataSource");
        
        private readonly ShedView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryController _inventoryController;
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

            _upgradeHandlersRepository = CreateRepository();
            _inventoryController = CreateInventoryController(placeForUi);

            _view = LoadView(placeForUi);
            
            _view.Init(Apply, Close, StartGame);
        }

        private IUpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryController CreateInventoryController(Transform placeForUi)
        {
            var inventoryController = new InventoryController(placeForUi, _profilePlayer.Inventory);
            AddController(inventoryController);

            return inventoryController;
        }

        protected override void OnDispose()
        {
            _view.Deinit();
            base.OnDispose();
        }


        private void Apply()
        {
            if (_appliedItems.Count == _profilePlayer.Inventory.EquippedItems.Count)
            {
                _view.Print("All Items Applied. Start the game");
                return;
            }
            
            UpgradeWithEquippedItems(
                _profilePlayer.CurrentTransport,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _view.Print("Apply. " +
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

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

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
                var temp = _appliedItems.Except(equippedItems).ToList();

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
        
        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);
            
            return objectView.GetComponent<ShedView>();
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}]: {message}");
    }
}