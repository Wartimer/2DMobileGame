using Profile;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;


internal class EntryPoint : MonoBehaviour
{
    private const float CarSpeed = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsService _unityAdsService;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private ProductLibrary _productLibrary;
    
    private GameStateController _gameStateController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(CarSpeed, InitialState);
        
        if (_unityAdsService.IsInitialized) OnAdsInitialized();
        else _unityAdsService.Initialized.AddListener(OnAdsInitialized);
        
        if (_iapService.IsInitialized) OnIapInitialized();
        else _iapService.Initialized.AddListener(OnIapInitialized);
        
        _analyticsManager.AnalyticsInit(profilePlayer);
        if(_analyticsManager.Initialized) OnAnalyticsManagerInitialized();
        else _analyticsManager.IsInitialized += OnAnalyticsManagerInitialized;
        
        
        _gameStateController = new GameStateController(_placeForUi, profilePlayer, _unityAdsService, _analyticsManager, _iapService, _productLibrary);
    }

    private void OnIapInitialized()
    {
        print("IAP service INITIALIZED");
    }
    
    private void OnAdsInitialized()
    {
        print("ADS service INITIALIZED");
        _unityAdsService.InterstitialPlayer.Play();
    }
    
    private void OnAnalyticsManagerInitialized()
    { 
        print("Analytics service INITIALIZED");
        _analyticsManager.SendMainMenuOpened();
    }
        
    private void OnDestroy()
    {
        _gameStateController.Dispose();
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _unityAdsService.Initialized.RemoveListener(OnAdsInitialized);
        _analyticsManager.IsInitialized -= OnAnalyticsManagerInitialized;
    }
}
