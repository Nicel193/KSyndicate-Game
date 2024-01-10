using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services.Ads
{
    public class AdsService : IUnityAdsListener, IAdsService
    {
        private const string AndroidGameId = "5522750";
        private const string IOSGameId = "5522751";

        private const string UnityRewardedVideoIdAndroid = "Rewarded_Android";
        private const string UnityRewardedVideoIdIOS = "Rewarded_iOS";

        private string _gameId;
        private string _placementId;

        private Action _onVideoFinished;
        private bool _rewardedVideoReady;

        public event Action RewardedVideoReady;

        public int Reward => 15;

        public void Initialize()
        {
            SetIdsForCurrentPlatform();
            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId);
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            _onVideoFinished = onVideoFinished;
            Advertisement.Show(_placementId);
        }

        public bool IsRewardedVideoReady => _rewardedVideoReady;

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady {placementId}");

            if (placementId == _placementId)
            {
                RewardedVideoReady?.Invoke();

                _rewardedVideoReady = true;
            }
        }

        public void OnUnityAdsDidError(string message) =>
            Debug.Log($"OnUnityAdsDidError {message}");

        public void OnUnityAdsDidStart(string placementId) =>
            Debug.Log($"OnUnityAdsDidStart {placementId}");

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Skipped:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Finished:
                    _onVideoFinished?.Invoke();
                    break;
                default:
                    Debug.LogError($"OnUnityAdsDidFinish {showResult}");
                    break;
            }

            _onVideoFinished = null;
        }

        private void SetIdsForCurrentPlatform()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    _placementId = UnityRewardedVideoIdAndroid;
                    break;

                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    _placementId = UnityRewardedVideoIdIOS;
                    break;

                case RuntimePlatform.WindowsEditor:
                    _gameId = IOSGameId;
                    _placementId = UnityRewardedVideoIdIOS;
                    break;

                default:
                    Debug.Log("Unsupported platform for ads.");
                    break;
            }
        }
    }
}