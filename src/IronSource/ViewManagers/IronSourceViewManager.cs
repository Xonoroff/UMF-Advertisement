using System.Threading;
using MF.Advertisement.src.Infrastructure.Entities;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using MF.Advertisement.src.IronSource.Managers;
using IronSourceRaw = global::IronSource.Scripts.IronSource;

namespace MF.Advertisement.src.IronSource.ViewManagers
{
    public class IronSourceViewManager : IAdsViewManager
    {
        public bool IsInitialized { get; private set; }
        
        private readonly IIronSourceManager ironSourceManager;

        private readonly IInterstitialViewManager interstitialViewManager;

        public IronSourceViewManager(IIronSourceManager ironSourceManager,
            IInterstitialViewManager interstitialViewManager)
        {
            this.ironSourceManager = ironSourceManager;
            this.interstitialViewManager = interstitialViewManager;
        }
        
        public async void Initialize()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                var dataEntity = await ironSourceManager.GetDataEntity(CancellationToken.None);
                IronSourceRaw.Agent.init(dataEntity.IronSourceAppKey);
                IronSourceRaw.Agent.validateIntegration();
                
                interstitialViewManager.Initialize();
            }
        }

        public void ShowAds(AdsContextEntity context)
        {
            if (context.AdsType == AdsType.Interstitial)
            {
                interstitialViewManager.Show(context.PlacementId);
            }
        }

        public void PreloadAds(AdsContextEntity context)
        {
            if (context.AdsType == AdsType.Interstitial)
            {
                interstitialViewManager.Load(context.PlacementId);
            }
        }
    }
}