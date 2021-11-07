using AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest;
using Core.src.Messaging;
using MF.Advertisement.src.Infrastructure.Entities;
using MF.Advertisement.src.Infrastructure.Messaging.RequestResponse.PreloadAds;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using Zenject;

namespace MF.Advertisement.src.AdsEditorMock.Installers
{
    public class AdsEditorMockSignalsInstaller : Installer<AdsEditorMockSignalsInstaller>
    {
        [Inject]
        private IEventBus eventBus;
        
        public override void InstallBindings()
        {
            eventBus.Subscribe<ShowAdsRequest>(ShowAdsHandler);    
            eventBus.Subscribe<PreloadAdsRequest>(PreloadAdsHandler);    
        }

        private async void ShowAdsHandler(ShowAdsRequest request)
        {
            var adsViewManager = Container.Resolve<IAdsViewManager>();
            var result = await adsViewManager.ShowAds(request.Context);

            request.Callback(new ShowAdsResponse(result));
        }
        
        private async void PreloadAdsHandler(PreloadAdsRequest request)
        {
            var adsViewManager = Container.Resolve<IAdsViewManager>();
            var preloadResult = await adsViewManager.PreloadAds(request.Context);
            
            request.Callback?.Invoke(new PreloadAdsResponse(preloadResult));
        }
    }
}