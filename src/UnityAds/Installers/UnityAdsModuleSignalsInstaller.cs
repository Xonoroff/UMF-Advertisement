#if UNITY_ADS_ENABLED
using AdsModule.src.Feature.Managers;
using AdsModule.src.Infrastructure.Messaging.RequestResponse.CheckVideo;
using AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest;
using Core.src.Messaging;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using Zenject;

namespace AdsModule.src.Feature.Installers
{
    public class UnityAdsModuleSignalsInstaller : Installer<UnityAdsModuleSignalsInstaller>
    {
        [Inject]
        private IEventBus eventBus;
        
        public override void InstallBindings()
        {
            eventBus.Subscribe<ShowAdsVideoRequest>(HandleShowAdsRequest);
            eventBus.Subscribe<CheckAdsVideoRequest>(HandleCheckAdsRequest);
            SceneManager.sceneLoaded += (arg0, mode) =>
            {
                var viewManager = Container.Resolve<IUnityAdsViewManager>();
                if (!viewManager.IsInitialized)
                {
                    viewManager.Initialize();
                }
            };
        }

        private void HandleCheckAdsRequest(CheckAdsVideoRequest request)
        {
            var adsViewManager = Container.Resolve<IUnityAdsViewManager>();
            var result = adsViewManager.IsVideoReady(request.PlacementId);
            var response = new CheckAdsVideoResponse()
            {
                IsReady = result
            };

            request.Callback(response);
        }

        private void HandleShowAdsRequest(ShowAdsVideoRequest request)
        {
            var adsViewManager = Container.Resolve<IUnityAdsViewManager>();
            var response = new ShowAdsVideoResponse();

            Subscribe();
            void Subscribe()
            {
                adsViewManager.OnUnityAdsDidErrorEvent += OnAdsError;
                adsViewManager.OnUnityAdsDidStartEvent += OnAdsStart;
                adsViewManager.OnUnityAdsDidFinishEvent += OnAdsFinish;
            }

            void UnSubscribe()
            {
                adsViewManager.OnUnityAdsDidErrorEvent -= OnAdsError;
                adsViewManager.OnUnityAdsDidStartEvent -= OnAdsStart;
                adsViewManager.OnUnityAdsDidFinishEvent -= OnAdsFinish;
            }
            
            void OnAdsError(string message)
            {
                UnSubscribe();
                response.VideoState = AdsVideoResult.Error;
                response.Data = message;
                request.Callback(response);
            }

            void OnAdsStart(string message)
            {
                var data = message;
            }

            void OnAdsFinish(string data, ShowResult result)
            {
                UnSubscribe();
                response.VideoState = AdsVideoResult.Shown;
                response.Data = data;
                request.Callback(response);
            }
            
            adsViewManager.ShowVideo(request.PlacementId);
        }
    }
}
#endif