using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Ui
{
    internal sealed class CarSelectView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBackToMainMenu;
        [SerializeField] private Button _startButton;

        [SerializeField] private Button _redCarButton;
        [SerializeField] private Button _schoolBusButton;

        [SerializeField] private TextMeshProUGUI _text;

        internal TextMeshProUGUI Text => _text; 

        public void ReturnToMainMenuInit(UnityAction returnToMainMenu) =>
            _buttonBackToMainMenu.onClick.AddListener(returnToMainMenu);
        
        public void StartGameInit(UnityAction startGame) =>
            _startButton.onClick.AddListener(startGame);

        public void SelectRedCar(UnityAction selectRedCar) =>
            _redCarButton.onClick.AddListener(selectRedCar);

        public void SelectSchoolBus(UnityAction selectSchoolBus) =>
            _schoolBusButton.onClick.AddListener(selectSchoolBus);
        
        public void OnDestroy() =>
            _buttonBackToMainMenu.onClick.RemoveAllListeners();
    }
}