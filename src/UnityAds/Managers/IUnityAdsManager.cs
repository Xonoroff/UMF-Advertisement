#if UNITY_ADS_ENABLED
using System.Threading.Tasks;
using AdsModule.src.Feature.Entities;

namespace AdsModule.src.Feature.Managers
{
    public interface IUnityAdsManager
    {
        Task<UnityAdsViewEntity> GetViewEntity();
    }
}

#endif