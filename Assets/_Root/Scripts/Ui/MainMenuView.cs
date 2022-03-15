using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonShowRewarderAds;
        [SerializeField] private Button _buttonBuySmthng;


        public void CarSelectInit(UnityAction startGame) =>
            _buttonStart.onClick.AddListener(startGame);

        public void OpenSettingsInit(UnityAction openSettings) =>
            _buttonSettings.onClick.AddListener(openSettings);

        public void ShowAdsInit(UnityAction showRewarded) =>
            _buttonShowRewarderAds.onClick.AddListener(showRewarded);

        public void BuySmthBtnInit(UnityAction buySmth)
        {
            _buttonBuySmthng.onClick.AddListener(buySmth);
        }
        
        public void OnDestroy() {

            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonShowRewarderAds.onClick.RemoveAllListeners();
        }
}
}
