#if UNITY_ADS_ENABLED
using System;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using UnityEngine.Advertisements;

namespace AdsModule.src.Feature.Managers
{
    public class UnityAdsViewManager : IAdsViewManager, IUnityAdsListener
    {
        private readonly IUnityAdsManager unityAdsManager;

        public bool IsInitialized { get; private set; }
    
        public event Action<string> OnAdsReady;
        public event Action<string> OnAdsErrorE;
        public event Action<string> OnAdsStart;
        public event Action<string, ShowResult> OnAdsFinish;
    
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
            OnAdsReady?.Invoke(placementId);
        }

        public void OnUnityAdsDidError(string message)
        {
            OnAdsErrorE?.Invoke(message);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            OnAdsStart?.Invoke(placementId);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            OnAdsFinish?.Invoke(placementId, showResult);
        }
    }
}
#endif