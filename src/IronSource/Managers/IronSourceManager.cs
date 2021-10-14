using System.Threading;
using AdsModule.src.Infrastructure.Messaging.Providers;
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.IronSource.Entities;
using MF.Advertisement.src.IronSource.Managers;

public class IronSourceManager : IIronSourceManager
{
    private IAdsConfigProvider adsConfigProvider;

    public IronSourceManager(IAdsConfigProvider adsConfigProvider)
    {
        this.adsConfigProvider = adsConfigProvider;
    }
    
    public async UniTask<IronSourceDataEntity> GetDataEntity(CancellationToken ct)
    {
        var config = await adsConfigProvider.GetConfig();

        return new IronSourceDataEntity()
        {
            IronSourceAppKey = config.GameId,
        };
    }
}
