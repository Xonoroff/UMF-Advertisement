using Core.src.Signals;
using MF.Advertisement.src.Infrastructure.Entities;

namespace AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest
{
    public class ShowAdsResponse : SignalBaseWithParameter<AdsResultEntity>
    {
        public ShowAdsResponse(AdsResultEntity model) : base(model)
        {
        }
    }
}