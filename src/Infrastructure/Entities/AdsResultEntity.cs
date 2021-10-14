using System.Collections.Generic;

namespace MF.Advertisement.src.Infrastructure.Entities
{
    public class AdsResultEntity
    {
        public string PlacementId { get; set; }
        
        public AdsResultState ResultState { get; set; }
            
        public List<KeyValuePair<string,string>> Rewards { get; set; }
    }
}