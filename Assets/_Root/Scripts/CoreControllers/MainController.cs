using Ui;
using Game;
using Inventory.Items;
using UnityEngine;
using Services.Ads;
using Services.IAP;
using Scripts.Enums;
using Services.Analytics;
using Shed.Upgrade;
using Tool;

internal class MainController : BaseController
{
    private readonly ResourcePath _upgradeHandlersDataSourcePath = new ResourcePath("Configs/Upgrades/UpgradeItemConfigDataSource");
    private readonly ResourcePath _itemsConfigDataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
    private readonly ResourcePath _shedViewPath = new ResourcePath("Prefabs/UI/ShedView");
    private readonly ResourcePath _invetoryViewPath = new ResourcePath("Prefabs/UI/InventoryView");
    
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private CarSelectController _carSelectController;
    private ShedController _shedController;
    private GameController _gameController;
    
    private AnalyticsManager _analyticsManager;
    private IAdsService _unityAdsService;
    private IIAPService _iapService;
    private ProductLibrary _productLibrary;
    private TransportType _transportType;

    public MainController(
                            Transform placeForUi, 
                            ProfilePlayer profilePlayer, 
                            IAdsService unityAdsService, 
                            AnalyticsManager analyticsManager,
                            IIAPService iapService,
                            ProductLibrary productLibrary)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _unityAdsService = unityAdsService;
        _analyticsManager = analyticsManager;
        _iapService = iapService;
        _productLibrary = productLibrary;
        
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    private void OnChangeGameState(GameState state)
    {
        DisposeAllControllers();
        
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _unityAdsService, _iapService, _productLibrary);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _analyticsManager, _placeForUi);
                break;
            case GameState.SelectCar:
                _carSelectController = new CarSelectController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, 
                                                     _profilePlayer, 
                                                     CreateUpgradeHandlersRepository(),
                                                     LoadShedView(_placeForUi),
                                                     CreateInventoryController(_placeForUi));
                break;
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
    
    private ShedView LoadShedView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_shedViewPath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi);
            
        return objectView.GetComponent<ShedView>();
    }
    
    private InventoryView LoadInventoryView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_invetoryViewPath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi);
            
        return objectView.GetComponent<InventoryView>();
    }
    
    
    
    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsMenuController?.Dispose();
        _carSelectController?.Dispose();
        _shedController?.Dispose();
    }
    
    protected override void OnDispose()
    {
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }
}
