using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Ads;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        private const int Reward = 15;
        
        public Button ShowButton;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;

        private IAdsService _adsService;
        private IPersistentProgressService _persistentProgressService;

        public void Construct(IAdsService adsService, IPersistentProgressService persistentProgressService)
        {
            _adsService = adsService;
            _persistentProgressService = persistentProgressService;
        }

        public void Initialize()
        {
            ShowButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Subscribe()
        {
            _adsService.RewardedVideoReady += RefreshAvailableAd;
        }

        public void Cleanup()
        {
            _adsService.RewardedVideoReady -= RefreshAvailableAd;
        }

        private void OnShowAdClicked()
        {
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _persistentProgressService.Progress.WorldData.LootData.Collected += Reward;
            
            Debug.Log(_persistentProgressService.Progress.WorldData.LootData.Collected);
        }

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject adActiveObject in AdActiveObjects)
                adActiveObject.SetActive(videoReady);

            foreach (GameObject adInactiveObject in AdInactiveObjects)
                adInactiveObject.SetActive(!videoReady);
        }
    }
}