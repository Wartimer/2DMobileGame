using System;
using UnityEngine;

namespace _Rewards
{
    [Serializable]
    internal class Reward
    {
        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public Sprite IconCurrency { get; private set; }
        [field: SerializeField] public int CountCurrency { get; private set; }
        
        [field: SerializeField] public bool Claimed  { get; set; }
    }
}