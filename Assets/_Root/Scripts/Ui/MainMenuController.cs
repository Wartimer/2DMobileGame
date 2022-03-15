using Profile;
using Services.Ads;
using Services.Analytics;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class MainMenuController : BaseController
    {
        private readonly ResourcePath _mainMenuViewPath = new ResourcePath("Prefabs/UI/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private AnalyticsManager _analyticsManager;
        private IAdsService _unityAdsService;
        private IIAPService _iapService;
        private ProductLibrary _productLibrary;
        

        public MainMenuController(Transform placeForUi, 
                                ProfilePlayer profilePlayer,
                                IAdsService unityAdsService, 
                                IIAPService iapService, 
                                ProductLibrary productLibrary)
        {
            _profilePlayer = profilePlayer;
            _unityAdsService = unityAdsService;
            _iapService = iapService;
            _productLibrary = productLibrary;
            _view = LoadView(placeForUi);
            
            _view.CarSelectInit(CarSelect);
            _view.OpenSettingsInit(Settings);
            _view.ShowAdsInit(_unityAdsService.RewardedPlayer.Play);
            _view.BuySmthBtnInit(BuyItem0);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_mainMenuViewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void CarSelect() =>
            _profilePlayer.CurrentState.Value = GameState.SelectCar;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void BuyItem0() =>
            _iapService.Buy(_productLibrary.Products[0].Id);

    }
}
