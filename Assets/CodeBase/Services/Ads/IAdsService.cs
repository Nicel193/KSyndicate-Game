using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Ads
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}