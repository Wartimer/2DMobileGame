using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToMainMenu;

        public void Init(UnityAction returnToMainMenu) =>
            _buttonBackToMainMenu.onClick.AddListener(returnToMainMenu);
        
        public void OnDestroy() =>
            _buttonBackToMainMenu.onClick.RemoveAllListeners();
    }
}