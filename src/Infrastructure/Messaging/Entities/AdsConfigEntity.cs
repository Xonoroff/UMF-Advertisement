using Core.src.Entity;

namespace AdsModule.src.Feature.Entities
{
    public class AdsConfigEntity : BaseEntity
    {
        public string GameId { get; set; }
        
        public bool IsTestMode { get; set; }
    }
}
