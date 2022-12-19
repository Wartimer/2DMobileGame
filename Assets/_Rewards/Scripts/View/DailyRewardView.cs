using System;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Rewards
{
    internal sealed class DailyRewardView : MonoBehaviour
    {
        private const string CurrentSlotIsActiveKey = nameof(CurrentSlotIsActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);
        private const string TimeDeadLineKey = nameof(TimeDeadLineKey);
        
        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public float TimeCoolDown { get; private set; } = 86400;
        [field: SerializeField] public float TimeToDeadline { get; private set; } = 172800;
        
        [field: Header("Settings Rewards")]
        [field: SerializeField] public List<Reward> Rewards { get; private set; }
        
        [field: Header("UI Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public TMP_Text TimerDeadLine { get; private set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }


        public int CurrentSlotIsActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotIsActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotIsActiveKey, value);
        }
        
        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey, null);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : (DateTime?) null;
            }
            set
            {
                if(value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }

        public DateTime? TimeDeadLineStart
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeDeadLineKey, null);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : (DateTime?) null;
            }
            set
            {
                if(value != null)
                    PlayerPrefs.SetString(TimeDeadLineKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeDeadLineKey);
            }
        }
    }
}