using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AbilitySystem.Abilities
{
    public class AbilityButtonView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;


        private void OnDestroy() => Deinit();


        public void Init(Sprite icon, UnityAction click)
        {
            _icon.sprite = icon;
            _button.onClick.AddListener(click);
        }

        public void Deinit()
        {
            _icon = null;
            _button.onClick.RemoveAllListeners();
        }
    }
}