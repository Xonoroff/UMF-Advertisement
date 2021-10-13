#if UNITY_ADS_ENABLED
using System.Threading.Tasks;
using AdsModule.src.Feature.Entities;
using AdsModule.src.Infrastructure.Messaging.Providers;

namespace AdsModule.src.Feature.Managers
{
    public class UnityAdsManager : IUnityAdsManager
    {
        private readonly IAdsConfigProvider adsConfigProvider;
        
        public UnityAdsManager(IAdsConfigProvider adsConfigProvider)
        {
            this.adsConfigProvider = adsConfigProvider;
        }
        
        public async Task<UnityAdsViewEntity> GetViewEntity()
        {
            var balance = await adsConfigProvider.GetConfig();
            var viewEntity = new UnityAdsViewEntity()
            {
                GameId = balance.GameId,
                IsTestMode = balance.IsTestMode,
            };

            return viewEntity;
        }
    }
}
#endif