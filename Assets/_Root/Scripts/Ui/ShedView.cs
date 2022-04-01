using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal interface IShedView
    {
        void Init(UnityAction apply, UnityAction back, UnityAction startGame);
        void Deinit();
    }

    internal class ShedView : MonoBehaviour, IShedView
    {
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private TextMeshProUGUI _text;


        private void OnDestroy() => Deinit();

        public void Init(UnityAction apply, UnityAction back, UnityAction startGame)
        {
            _buttonApply.onClick.AddListener(apply);
            _buttonBack.onClick.AddListener(back);
            _buttonStart.onClick.AddListener(startGame);
        }


        public void Print(string message)
        {
            _text.text = message;
        }

        internal void ButtonApplySetInteractable(bool value)=>
                _buttonApply.interactable = !value;
        
        public void Deinit()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonBack.onClick.RemoveAllListeners();
            _buttonStart.onClick.RemoveAllListeners();
        }
    }
}