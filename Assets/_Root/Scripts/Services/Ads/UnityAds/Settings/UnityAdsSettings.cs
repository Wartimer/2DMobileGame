using UnityEngine;
using Services.Ads.UnityAds.Settings;

namespace Services.Ads.UnityAds
{
    [CreateAssetMenu(fileName = nameof(UnityAdsSettings), menuName = "Settings/Ads/" + nameof(UnityAdsSettings))]
    internal class UnityAdsSettings : ScriptableObject
    {
        [Header("Game ID")] 
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iOsGameId;
        
        
        [field: Header("Players")]
        [field: SerializeField] public AdsPlayerConfig Interstitial { get; private set; }
        [field: SerializeField] public AdsPlayerConfig Rewarded { get; private set; }
        [field: SerializeField] public AdsPlayerConfig Banner { get; private set; }

        [field: Header("Settings")]
        [field: SerializeField] public bool TestMode { get; private set; } = true;
        [field: SerializeField] public bool EnablePerPlacementMode { get; private set; } = true;

        public string GameId =>
#if UNITY_EDITOR
            _androidGameId;
#else
            Application.platform switch
            {
                RuntimePlatform.Android => _androidGameId,
                RuntimePlatform.IPhonePlayer => _iOsGameId,
                _ => ""
            };
#endif
    }
}