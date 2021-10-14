using Core.src.Messaging;
using MF.Advertisement.src.Infrastructure.Entities;

namespace AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest
{
    public class ShowAdsRequest : EventBusRequest<ShowAdsResponse>
    {
        public AdsContextEntity Context { get; set; }
    }
}
