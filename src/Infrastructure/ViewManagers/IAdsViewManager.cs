using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.Infrastructure.Entities;

namespace MF.Advertisement.src.Infrastructure.ViewManagers
{
    public interface IAdsViewManager
    {
        bool IsInitialized { get; }
        
        UniTask<bool> Initialize();
        
        UniTask<AdsResultEntity> ShowAds(AdsContextEntity context);
        
        UniTask<bool> PreloadAds(AdsContextEntity context);
    }
}