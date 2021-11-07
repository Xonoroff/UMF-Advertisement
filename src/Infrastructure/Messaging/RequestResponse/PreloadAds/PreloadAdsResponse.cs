namespace MF.Advertisement.src.Infrastructure.Messaging.RequestResponse.PreloadAds
{
    public class PreloadAdsResponse
    {
        public bool Result { get; }

        public PreloadAdsResponse(bool result)
        {
            Result = result;
        }
    }
}