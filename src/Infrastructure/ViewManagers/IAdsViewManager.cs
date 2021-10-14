using System;
using System.Collections.Generic;
using MF.Advertisement.src.Infrastructure.Entities;

namespace MF.Advertisement.src.Infrastructure.ViewManagers
{
    public interface IAdsViewManager
    {
        bool IsInitialized { get; }
        
        void Initialize();
        
        void ShowAds(AdsContextEntity context);
        
        void PreloadAds(AdsContextEntity context);
    }
}