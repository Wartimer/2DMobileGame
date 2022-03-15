using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;


        public void CarSelectInit(UnityAction startGame) =>
            _buttonStart.onClick.AddListener(startGame);

        public void OpenSettingsInit(UnityAction openSettings) =>
            _buttonSettings.onClick.AddListener(openSettings);

        public void OnDestroy() {

            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
        }
}
}
