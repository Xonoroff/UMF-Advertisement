#if UNITY_ADS_ENABLED
using System;
using UnityEngine.Advertisements;

namespace AdsModule.src.Feature.Managers
{
    public interface IUnityAdsViewManager
    {
        bool IsInitialized { get; }
        
        event Action<string> OnUnityAdsReadyEvent;
        event Action<string> OnUnityAdsDidErrorEvent;
        event Action<string> OnUnityAdsDidStartEvent;
        event Action<string, ShowResult> OnUnityAdsDidFinishEvent;

        void Initialize();
        
        void ShowVideo(string placementId);

        bool IsVideoReady(string placementId);
    }
}
#endif