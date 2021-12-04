using System.Threading;
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.Infrastructure.Entities;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using MF.Advertisement.src.IronSource.Managers;

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
        
        public async UniTask<bool> Initialize()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                var dataEntity = await ironSourceManager.GetDataEntity(CancellationToken.None);
                global::IronSource.Agent.init(dataEntity.IronSourceAppKey);
                global::IronSource.Agent.validateIntegration();
                
                interstitialViewManager.Initialize();

                return true;
            }

            return true;
        }

        public async UniTask<AdsResultEntity> ShowAds(AdsContextEntity context)
        {
            if (context.AdsType == AdsType.Interstitial)
            {
                var result = await interstitialViewManager.Show(context.PlacementId);
                return result;
            }

            return new AdsResultEntity()
            {
                Rewards = null,
                PlacementId = context.PlacementId,
                ResultState = AdsResultState.Error,
            };
        }

        public async UniTask<bool> PreloadAds(AdsContextEntity context)
        {
            if (context.AdsType == AdsType.Interstitial)
            {
                var result = await interstitialViewManager.Load(context.PlacementId);
                return result;
            }

            return false;
        }
    }
}