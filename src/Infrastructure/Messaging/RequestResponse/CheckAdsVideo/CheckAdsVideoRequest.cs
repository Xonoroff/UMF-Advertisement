using Core.src.Messaging;

namespace AdsModule.src.Infrastructure.Messaging.RequestResponse.CheckVideo
{
    public class CheckAdsVideoRequest : EventBusRequest<CheckAdsVideoResponse>
    {
        public string PlacementId { get; set; }
    }
}