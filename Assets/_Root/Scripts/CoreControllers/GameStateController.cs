using Ui;
using Game;
using Scripts.Enums;
using Services.Ads;
using Services.Analytics;
using Services.IAP;
using UnityEngine;

internal class GameStateController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private CarSelectController _carSelectController;
    private ShedController _shedController;
    
    private GameInitController _gameInitController;
    private AnalyticsManager _analyticsManager;
    private IAdsService _unityAdsService;
    private IIAPService _iapService;
    private ProductLibrary _productLibrary;
    private TransportType _transportType;

    public GameStateController(
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
                _gameInitController = new GameInitController(_profilePlayer, _analyticsManager, _placeForUi);
                break;
            case GameState.SelectCar:
                _carSelectController = new CarSelectController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, _profilePlayer);
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }

    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameInitController?.Dispose();
        _settingsMenuController?.Dispose();
        _carSelectController?.Dispose();
        _shedController?.Dispose();
    }
    
    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameInitController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }
}
