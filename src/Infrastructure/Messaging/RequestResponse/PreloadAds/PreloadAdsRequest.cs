using Core.src.Messaging;
using MF.Advertisement.src.Infrastructure.Entities;

namespace MF.Advertisement.src.Infrastructure.Messaging.RequestResponse.PreloadAds
{
    public class PreloadAdsRequest : EventBusRequest<PreloadAdsResponse>
    {
        public AdsContextEntity Context { get; set; }
    }
}