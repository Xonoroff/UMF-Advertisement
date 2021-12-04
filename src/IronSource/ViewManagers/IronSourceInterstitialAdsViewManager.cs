
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.Infrastructure.Entities;
using UnityEngine;

namespace MF.Advertisement.src.IronSource.ViewManagers
{
    public interface IInterstitialViewManager
    {
        void Initialize();

        UniTask<bool> Load(string placementId = null);
        
        UniTask<AdsResultEntity> Show(string placementId = null);
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
            
            global::IronSource.Agent.loadInterstitial();

            return waitingSource.Task;
        }

        public UniTask<AdsResultEntity> Show(string placementId = null)
        {
            if (!isShowReady)
            {
                Debug.LogError($"Iron source show not available");
                var result = new AdsResultEntity()
                {
                    PlacementId = placementId,
                    ResultState = AdsResultState.WasNotReady,
                };
                
                return UniTask.FromResult(result);
            }

            var completionSource = new UniTaskCompletionSource<AdsResultEntity>();
            if (!string.IsNullOrEmpty(placementId))
            {
                if (global::IronSource.Agent.isInterstitialPlacementCapped(placementId))
                {
                    var result = new AdsResultEntity()
                    {
                        PlacementId = placementId,
                        ResultState = AdsResultState.Capped,
                    };
                    
                    Debug.Log($"Iron source ads cap for placement {placementId}");
                    return UniTask.FromResult(result);
                }        
                
                isShowReady = false;
                global::IronSource.Agent.showInterstitial(placementId);
            }
            else
            {
                isShowReady = false;
                global::IronSource.Agent.showInterstitial();
            }

            void OnShown()
            {
                DisposeSubscribers();
                var result = new AdsResultEntity()
                {
                    PlacementId = placementId,
                    ResultState = AdsResultState.Shown,
                };
                
                completionSource.TrySetResult(result);
            }

            void OnFailed(IronSourceError error)
            {
                var result = new AdsResultEntity()
                {
                    Rewards = null,
                    PlacementId = placementId,
                    ResultState = AdsResultState.Failed,
                };
                
                Debug.LogError($"Can't show ad");
                DisposeSubscribers();
                completionSource.TrySetResult(result);
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