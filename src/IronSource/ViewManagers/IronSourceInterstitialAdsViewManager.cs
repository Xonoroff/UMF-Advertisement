using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IronSource.Scripts;
using UnityEngine;
using IronSourceRaw = global::IronSource.Scripts.IronSource;

namespace MF.Advertisement.src.IronSource.ViewManagers
{
    public interface IInterstitialViewManager
    {
        void Initialize();

        UniTask<bool> Load(string placementId = null);
        
        UniTask<bool> Show(string placementId = null);
    }
    
    public class IronSourceInterstitialAdsViewManager : IInterstitialViewManager
    {
        private bool isLoadingAvailable = true;

        private bool isShowReady = false;
        
        public void Initialize()
        {
            //TODO: Not sure about this event tracking
            //IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += () => { isLoadingAvailable = true; };
        }


        public UniTask<bool> Load(string placementId = null)
        {
            if (!isLoadingAvailable)
            {
                return UniTask.FromResult(false);
            }
            
            isLoadingAvailable = false;
            var waitingSource = new UniTaskCompletionSource<bool>();

            void DisposeSubscriptions()
            {
                IronSourceEvents.onInterstitialAdReadyEvent -= CompleteSourceSuccess;
                IronSourceEvents.onInterstitialAdLoadFailedEvent -= CompleteSourceFail;
            }
            void CompleteSourceSuccess()
            {
                isShowReady = true;
                CompleteSource(true);
            }

            void CompleteSourceFail(IronSourceError ironSourceError)
            {
                isShowReady = false;
                Debug.LogError($"Can't load iron source interstitial ad");
                CompleteSource(false);
            }
            
            void CompleteSource(bool isSuccess)
            {
                DisposeSubscriptions();
                waitingSource.TrySetResult(isSuccess);
            }

            IronSourceEvents.onInterstitialAdReadyEvent += CompleteSourceSuccess;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += CompleteSourceFail;
            
            IronSourceRaw.Agent.loadInterstitial();

            return waitingSource.Task;
        }

        public UniTask<bool> Show(string placementId = null)
        {
            if (!isShowReady)
            {
                Debug.LogError($"Iron source show not available");
                return UniTask.FromResult(false);
            }

            var completionSource = new UniTaskCompletionSource<bool>();
            if (!string.IsNullOrEmpty(placementId))
            {
                if (IronSourceRaw.Agent.isInterstitialPlacementCapped(placementId))
                {
                    Debug.Log($"Iron source ads cap for placement {placementId}");
                    return UniTask.FromResult(false);
                }        
                
                isShowReady = false;
                IronSourceRaw.Agent.showInterstitial(placementId);
            }
            else
            {
                isShowReady = false;
                IronSourceRaw.Agent.showInterstitial();
            }

            void OnShown()
            {
                DisposeSubscribers();
                completionSource.TrySetResult(true);
            }

            void OnFailed(IronSourceError error)
            {
                Debug.LogError($"Can't show ad");
                DisposeSubscribers();
                completionSource.TrySetResult(false);
            }

            void DisposeSubscribers()
            {
                IronSourceEvents.onInterstitialAdOpenedEvent -= OnShown;
                IronSourceEvents.onInterstitialAdShowFailedEvent -= OnFailed;
            }
            
            IronSourceEvents.onInterstitialAdOpenedEvent += OnShown;
            IronSourceEvents.onInterstitialAdShowFailedEvent += OnFailed;

            //not sure about implementation
            //IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;

            return completionSource.Task;
        }
    }
}