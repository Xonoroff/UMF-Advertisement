using AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest;
using Core.src.Messaging;
using MF.Advertisement.src.Infrastructure.Entities;
using MF.Advertisement.src.Infrastructure.Messaging.RequestResponse.PreloadAds;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using Zenject;

namespace MF.Advertisement.src.IronSource.Installers
{
    public class IronSourceSignalsInstaller : Installer<IronSourceSignalsInstaller>
    {
        [Inject]
        private IEventBus eventBus;
        
        public override void InstallBindings()
        {
            eventBus.Subscribe<ShowAdsRequest>(ShowAdsHandler);    
            eventBus.Subscribe<PreloadAdsRequest>(PreloadAdsHandler);    
        }

        private void ShowAdsHandler(ShowAdsRequest request)
        {
            var adsViewManager = Container.Resolve<IAdsViewManager>();
            adsViewManager.ShowAds(request.Context);

            //TODO: Implement
            request.Callback(new ShowAdsResponse(new AdsResultEntity()));
        }
        
        private async void PreloadAdsHandler(PreloadAdsRequest request)
        {
            var adsViewManager = Container.Resolve<IAdsViewManager>();
            adsViewManager.PreloadAds(request.Context);
            
            request.Callback?.Invoke(new PreloadAdsResponse());
        }
    }
}