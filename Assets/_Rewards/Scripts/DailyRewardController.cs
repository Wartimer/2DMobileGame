using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Rewards
{
    internal sealed class DailyRewardController
    {
        private const string ClaimedRewardsCountKey = nameof(ClaimedRewardsCountKey);
        
        private readonly DailyRewardView _dailyRewardView;
        
        private List<ContainerSlotRewardView> _rewardSlots;
        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;
        private bool _allRewardsGathered;
        
        public DailyRewardController(DailyRewardView dailyRewardView) =>
            _dailyRewardView = dailyRewardView;
        
        public int ClaimedRewardsCount
        {
            get => PlayerPrefs.GetInt(ClaimedRewardsCountKey);
            set => PlayerPrefs.SetInt(ClaimedRewardsCountKey, value);
        }

        public void Init()
        {
            if (_isInitialized) return;
            ClaimedRewardsCount = 0;
            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();
            _isInitialized = true;
        }

        public void DeInit()
        {
            if (!_isInitialized) return;

            DeInitSlots();
            StopRewardsUpdating();
            
            UnsubscribeButtons();
            _isInitialized = false;
        }

        private void InitSlots()
        {
            _rewardSlots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreatSlotRewardView();
                _rewardSlots.Add(instanceSlot);
            }
        }

        private void DeInitSlots()
        {
            foreach (var rewardSlot in _rewardSlots)
                Object.Destroy(rewardSlot.gameObject);

            _rewardSlots.Clear();
        }
        
        private void StartRewardsUpdating() =>
            _coroutine = _dailyRewardView.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;
            _dailyRewardView.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);
           
            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }
        
        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _dailyRewardView.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;
            
            var isDeadlineElapsed = timeFromLastRewardGetting.Seconds >= _dailyRewardView.TimeToDeadline;
            
            var isTimeToGetNewReward = timeFromLastRewardGetting.Seconds >= _dailyRewardView.TimeCoolDown;

            if (isDeadlineElapsed)
            {
                ResetRewardsState();
                ClaimedRewardsCount = 0;
            }

            if (_dailyRewardView.Rewards.Count == ClaimedRewardsCount)
            {
                _isGetReward = false;
            }
            else
            {
                _isGetReward = isTimeToGetNewReward;
                
            }
        }

        private void RefreshUi()
        {
            _dailyRewardView.GetRewardButton.interactable = _isGetReward;

            _dailyRewardView.TimerNewReward.text = GetNewRewardTimerText();
            
            if(ClaimedRewardsCount != 0)
                _dailyRewardView.TimerDeadLine.text = GetDeadLineTimerText();

            RefreshRewardSlots();
        }
        
        private void RefreshRewardSlots()
        {
            for (int i = 0; i < _rewardSlots.Count; i++)
            {
                Reward reward = _dailyRewardView.Rewards[i];
                int countDay = i + 1;
                bool isSelect = i == _dailyRewardView.CurrentSlotIsActive && _isGetReward;
                
                _rewardSlots[i].SetData(reward, countDay, isSelect);
            }
        }
        
        private void ResetRewardsState()
        {
            _dailyRewardView.TimeGetReward = null;
            _dailyRewardView.TimeDeadLineStart = null;
            _dailyRewardView.TimerDeadLine.text = string.Empty;
            _dailyRewardView.CurrentSlotIsActive = 0;
        }

        private string GetNewRewardTimerText()
        {
       
            if (_isGetReward)
                return "The reward is ready to be received";

            if (ClaimedRewardsCount == _rewardSlots.Count)
                return "All Rewards are gathered";

            if (_dailyRewardView.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime =
                    _dailyRewardView.TimeGetReward.Value
                        .AddSeconds(_dailyRewardView.TimeCoolDown);
                TimeSpan currentClaimCoolDown = nextClaimTime - DateTime.UtcNow;
                string timeGetReward = currentClaimCoolDown.ToString(@"d\.hh\:mm\:ss");

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }

        private string GetDeadLineTimerText()
        {
            
            if (_dailyRewardView.TimeDeadLineStart.HasValue)
            {
                DateTime nextResetTime =
                    _dailyRewardView.TimeDeadLineStart.Value
                        .AddSeconds(_dailyRewardView.TimeToDeadline);
                TimeSpan currentResetCoolDown = nextResetTime - DateTime.UtcNow;
                string timeDeadLine = currentResetCoolDown.ToString(@"d\.hh\:mm\:ss");
                return $"Time to reset rewards: {timeDeadLine}";
            }

            return string.Empty;
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;
            
            Reward reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotIsActive];
            
            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    CurrencyView.Instance.AddWood(reward.CountCurrency);
                    
                    break;
                case RewardType.Diamond:
                    CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                    break;
            }

            ClaimedRewardsCount++;
            _dailyRewardView.CurrentSlotIsActive++;
            
            _dailyRewardView.TimeGetReward = DateTime.UtcNow;
            _dailyRewardView.TimeDeadLineStart = DateTime.UtcNow; 
            
            RefreshRewardsState();
        }

        private ContainerSlotRewardView CreatSlotRewardView()
        {
            return Object.Instantiate
            (
                _dailyRewardView.ContainerSlotRewardPrefab,
                _dailyRewardView.MountRootSlotsReward,
                false
            );
        }

        private void UnsubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.RemoveListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.RemoveListener(TotalReset);
        }


        private void SubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(TotalReset);
        }

        
        private void TotalReset()
        {
            PlayerPrefs.DeleteAll();
            ClaimedRewardsCount = 0;
            ResetRewardsState();
            RefreshRewardSlots();
        } 
    }
}
