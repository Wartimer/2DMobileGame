using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Rewards
{
    internal sealed class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textDay;
        [SerializeField] private TMP_Text _textDayCount;
        [SerializeField] private TMP_Text _countReward;
        
        
        public void SetData(Reward reward, int countDay, bool isSelect)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            _textDay.text = "День";
            _textDayCount.text = $"{countDay}";
            _countReward.text = reward.CountCurrency.ToString();
            
            UpdateBackground(isSelect);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}