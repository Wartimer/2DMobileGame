using System;
using UnityEngine;

namespace _Rewards
{
    internal class CurrencyView: MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        
        private static CurrencyView _instance;
        
        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currencyDiamond;

        public int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey, 0);
            private set
            {
                PlayerPrefs.SetInt(WoodKey, value);
            } 
        }

        public int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey, 0);
            private set
            {
                PlayerPrefs.SetInt(DiamondKey, value);
            }
        }

        public static CurrencyView Instance => _instance;
        
        private void Awake() => _instance = this;

        private void OnDestroy() => _instance = null;

        private void Start()
        {
            _currencyWood.SetData(Wood);
            _currencyDiamond.SetData(Diamond);
        }    
        
        public void AddWood(int value)
        {
            Wood += value;
            _currencyWood.SetData(Wood);
        }

        public void AddDiamond(int value)
        {
            Diamond += value;
            _currencyDiamond.SetData(Diamond);
        }
        
    }
}