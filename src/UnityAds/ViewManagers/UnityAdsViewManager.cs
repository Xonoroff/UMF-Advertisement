#if UNITY_ADS_ENABLED
using System;
using UnityEngine.Advertisements;

namespace AdsModule.src.Feature.Managers
{
    public class UnityAdsViewManager : IUnityAdsViewManager, IUnityAdsListener
    {
        private readonly IUnityAdsManager unityAdsManager;

        public bool IsInitialized { get; private set; }
    
        public event Action<string> OnUnityAdsReadyEvent;
        public event Action<string> OnUnityAdsDidErrorEvent;
        public event Action<string> OnUnityAdsDidStartEvent;
        public event Action<string, ShowResult> OnUnityAdsDidFinishEvent;
    
        public UnityAdsViewManager(IUnityAdsManager unityAdsManager)
        {
            this.unityAdsManager = unityAdsManager;
        }

        public async void Initialize()
        {
            var config = await unityAdsManager.GetViewEntity();
            Advertisement.Initialize(config.GameId, config.IsTestMode);
            Advertisement.AddListener(this);
            IsInitialized = true;
        }
    
        public void ShowVideo(string placementId)
        {
            if (!IsInitialized)
            {
                OnUnityAdsDidFinish(placementId, ShowResult.Failed);
            }
            else
            {
                Advertisement.Show(placementId);
            }
        }

        public bool IsVideoReady(string placementId)
        {
            return Advertisement.IsReady(placementId);
        }

        public void OnUnityAdsReady(string placementId)
        {
            OnUnityAdsReadyEvent?.Invoke(placementId);
        }

        public void OnUnityAdsDidError(string message)
        {
            OnUnityAdsDidErrorEvent?.Invoke(message);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            OnUnityAdsDidStartEvent?.Invoke(placementId);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            OnUnityAdsDidFinishEvent?.Invoke(placementId, showResult);
        }
    }
}
#endif