using System;
using UnityEngine;

namespace _Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private DailyRewardView _dailyRewardView;

        private DailyRewardController _dailyRewardController;

        private void Awake() =>
            _dailyRewardController = new DailyRewardController(_dailyRewardView);

        private void Start() =>
            _dailyRewardController.Init();

        private void OnDestroy() =>
            _dailyRewardController.DeInit();
    }
}