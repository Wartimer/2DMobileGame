using Profile.GameSettings;
using Scripts.Enums;
using Services.Ads.UnityAds;
using Services.Analytics;
using Services.IAP;
using UnityEngine;


internal class EntryPoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameStartSettings _gameStartSettings;
    GameState _initialState;

    [Header("Attachments")]
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsService _unityAdsService;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private ProductLibrary _productLibrary;
    
    private MainController _mainController;


    private void Awake()
    {
        _initialState = _gameStartSettings.GameState;
        var profilePlayer = new ProfilePlayer(_initialState);
        
        if (_unityAdsService.IsInitialized) OnAdsInitialized();
        else _unityAdsService.Initialized.AddListener(OnAdsInitialized);
        
        if (_iapService.IsInitialized) OnIapInitialized();
        else _iapService.Initialized.AddListener(OnIapInitialized);
        
        _analyticsManager.AnalyticsInit(profilePlayer);
        if(_analyticsManager.Initialized) OnAnalyticsManagerInitialized();
        else _analyticsManager.IsInitialized += OnAnalyticsManagerInitialized;
        
        
        _mainController = new MainController(_placeForUi, profilePlayer, _unityAdsService, _analyticsManager, _iapService, _productLibrary);
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
        _mainController.Dispose();
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _unityAdsService.Initialized.RemoveListener(OnAdsInitialized);
        _analyticsManager.IsInitialized -= OnAnalyticsManagerInitialized;
    }
}
