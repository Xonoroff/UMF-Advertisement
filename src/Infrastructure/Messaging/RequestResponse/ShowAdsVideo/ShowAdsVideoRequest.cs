using Core.src.Messaging;

namespace AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest
{
    public class ShowAdsVideoRequest : EventBusRequest<ShowAdsVideoResponse>
    {
        public string PlacementId { get; set; }
    }
}
