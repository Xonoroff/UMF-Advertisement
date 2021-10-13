#if UNITY_ADS_ENABLED
namespace AdsModule.src.Feature.Entities
{
    public class UnityAdsViewEntity
    {
        public string GameId { get; set; }
        
        public bool IsTestMode { get; set; }
    }
}
#endif